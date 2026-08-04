# Personal iOS Fitness App — Research Synthesis & Build Plan

> Synthesized from 21 parallel research agents (Aug 2026). Raw per-region findings live in
> [`regions/`](regions/). Two regions (`18-competitor-gaps`, `19-apple-native`) lost live web
> access mid-run and are from model knowledge — treat their sentiment claims as
> medium-confidence; everything else is web-verified with sources.

---

## 1. Feasibility — VERDICT: fully feasible, native-only

| Question | Answer |
|---|---|
| Install without App Store? | **Yes.** Xcode + free Apple ID ("Personal Team") → build straight to iPhone |
| The catch | Provisioning profile expires every **7 days** → weekly ~2-min re-deploy (works over Wi-Fi after first cable pairing). Max 3 sideloaded apps, 10 App IDs/week |
| PWA / web app? | **Dead end.** Web apps cannot touch HealthKit — no Apple Watch data, period |
| HealthKit on free account? | **Yes** — confirmed in Apple's official capabilities table. App Groups, Background Modes, MapKit, WidgetKit, Live Activities (local), App Intents all work too |
| NOT available free | Push notifications (APNs), iCloud/CloudKit, Siri capability, TestFlight. Use local notifications + local-only architecture |
| Data loss on expiry? | **None.** Expiry only blocks launch. App container survives re-deploys with same bundle ID. HealthKit data lives in the OS store — survives even full app deletion |
| Escape hatch | $99/yr Apple Developer Program → 1-year profiles + TestFlight (90-day builds, OTA) + push + iCloud |

**Golden rules from feasibility research:**
1. Pick ONE bundle ID (e.g. `com.anuj.fitrpg`) and never change it.
2. HealthKit is the durable database; app-local state must be recomputable from HealthKit history (XP/levels derived, not only stored).
3. Never design around APNs or CloudKit.

```
  APPLE WATCH ULTRA 3                          iPHONE
 ┌──────────────────────┐              ┌─────────────────────────────────────┐
 │ 3rd-gen optical HR   │              │            ┌──────────────┐         │
 │ ECG · SpO2 · temp    │   auto sync  │  HealthKit │  ── read ──▶ │ OUR APP │
 │ dual-freq L1+L5 GPS  │ ───────────▶ │  (system   │  (one-time   │ SwiftUI │
 │ barometer/altimeter  │              │   store)   │   grant)     │         │
 │ accel 800Hz · gyro   │              │            └──────────────┘         │
 │ depth · water temp   │              │      PWA ──✗── blocked              │
 └──────────────────────┘              └─────────────────────────────────────┘
```

---

## 2. Product thesis (from competitor-gap analysis)

> **"Everything they paywall, computed locally from your own wrist — with zero
> subscriptions, zero cloud, zero guilt, plus the RPG layer nobody ships."**

Top evidenced gaps this app exploits, ranked:

1. **Subscription fatigue** — power users pay $400–700/yr (Strava $80 + Whoop $239/Oura $72 + MFP $80 + recovery app). Every paywalled metric (readiness, heatmaps, training load, adaptive TDEE) is computable free from HealthKit.
2. **No true all-in-one** — users juggle 3–5 apps; even Apple splits Fitness/Health/Vitals. Apple computes calories-in AND calories-out but never shows them together.
3. **Guilt gamification** — ring/streak anxiety is documented churn; Gentler Streak won an Apple Design Award for the opposite. XP must be additive-only.
4. **Paywalled readiness** — Whoop/Oura sell an HRV+RHR+sleep z-score. Athlytic/Training Today prove a solo dev replicates it from HealthKit.
5. **UI clutter** — praise concentrates on minimal apps. Cream/coral minimalism is the counter-position.
6. **Fitness-RPG demand is real but unserved** — Solo-Leveling-style apps (Arise, LEVELING) validate the concept but are ad-ridden and self-reported. **Sensor-verified XP ("earned, not logged") is a differentiator none of them have.**

---

## 3. What the Ultra 3 gives us (data inventory)

Ultra 3 = same sensors as Ultra 2 (all Ultra 2 APIs apply) + satellite, 5G, brighter LTPO3, 42h battery.

**Auto-written to HealthKit, no code needed** (the app is a read-heavy client):
- **Heart**: heartRate (per-sec in workouts), restingHeartRate, HRV (SDNN), heartRateRecoveryOneMinute, VO2max, ECG samples, AFib burden
- **Energy/activity**: activeEnergyBurned, basalEnergyBurned, steps, distance, flights, exerciseTime, standTime, physicalEffort (MET-like, all day)
- **Running dynamics**: runningPower, runningSpeed, strideLength, groundContactTime, verticalOscillation
- **Workouts**: HKWorkout (~80 types) + per-workout stats + splits/events + **HKWorkoutRoute GPS** + estimatedWorkoutEffortScore (1–10)
- **Sleep**: stages (REM/core/deep/awake), respiratoryRate, appleSleepingWristTemperature (baseline-relative!), breathing disturbances, SpO2
- **Cycling**: power/cadence/speed/FTP (BT sensors)

**Locked away (don't plan on)**: raw PPG, live skin temp, ambient light, triggering ECG/SpO2 on demand, Apple's Sleep Score & Training Load numbers (inputs readable → recompute), satellite APIs, full-depth submersion (restricted entitlement).

**Read-only types** (can't write): appleExerciseTime, standTime, wrist temp, walkingHeartRateAverage, HR recovery.

**We write**: dietary* (nutrition is 100% app-written — Apple ships NO food logging), bodyMass, workoutEffortScore (manual RPE), optional workouts/routes.

---

## 4. The app — five tabs

```
┌─────────────────────────────────────────────────────────────────────┐
│  TODAY        TRAIN         FUEL          MAP           YOU         │
│  ─────        ─────         ────          ───           ───         │
│  unified      workouts +    calorie/      routes +      avatar,     │
│  dashboard    training      macro log,    lifetime      XP, stats,  │
│  (the "one    load, best    adaptive      heatmap,      badges,     │
│  screen")     efforts       TDEE          elevation     streak      │
└─────────────────────────────────────────────────────────────────────┘
```

### TODAY — the screen Apple never built
Max ~5 numbers (progressive disclosure). Cards: rings (real HKActivitySummary), calories in vs out, last-night sleep (score ring + hours + consistency), readiness band, XP progress. One dark feature card per screen for rhythm.

### TRAIN — Strava-Premium analytics, single-player
- Feed of Strava-style cards: map thumbnail (MKMapSnapshotter, cached PNG) + 3-stat row.
- Detail: hero map with **gradient polyline colored by pace/HR** (MKGradientPolylineRenderer), stat grid, splits as pace bars + table, pace/HR charts overlaid on shaded elevation profile, drag-to-scrub synced to map.
- **Best Efforts** (Strava's most-copied feature): fastest elapsed-time windows at 1K/1mi/5K/10K/half; top-3 lifetime + top-10 annual → PR medals + XP bonuses.
- **Fitness / Fatigue / Form**: Edwards TRIMP per workout (time-in-zone × 1..5) → CTL 42-day EWMA / ATL 7-day EWMA / TSB = CTL−ATL. Zones via Karvonen (%HRR, resting HR from HealthKit). This replicates Strava's paid "Fitness & Freshness".
- VO2max + FTP read from Apple (never recompute; show as trend, ~13% absolute error).
- Optional later: WorkoutKit pushes custom interval workouts INTO Apple's watch Workout app (Runna-style, zero watch code).

### FUEL — MacroFactor model, not MyFitnessPal
- Logging: recents/favorites/saved-meals first, copy-yesterday, quick-add kcal+macros, **free barcode scan** (VisionKit + Open Food Facts API), USDA FoodData Central search fallback. Target <15s per meal. Cache all hits locally.
- Budget: **adaptive TDEE** back-calculated from logged intake vs smoothed weight trend (slope × 7700 kcal/kg over 14–28d window). Mifflin-St Jeor cold-start seed. **Never "eat back" watch calories** (Watch overestimates energy ~28% per meta-analysis; HR is accurate, calories are not).
- Presentation: trend is the truth; daily numbers labeled estimates. **Adherence-neutral**: no red numbers, no failure states. Write dietary samples to HealthKit (with sync identifiers for clean edits).
- Skip photo-AI logging (~70% category accuracy, portions off by 400–700 kcal).

### MAP — the beloved paid features, free & private
- Per-workout routes (Ultra 3 dual-frequency accuracy flows in automatically via CLLocation).
- **Lifetime personal heatmap**: one custom MKOverlayRenderer drawing all cached, Douglas-Peucker-simplified polylines at low opacity — additive overdraw = Strava glow, in coral.
- Elevation: Swift Charts area over cumulative distance; ascent via 2m-hysteresis smoothing, cross-checked with HKMetadataKeyElevationAscended.
- GPX export (CoreGPX) via entitlement-free fileExporter. MapKit only (no keys, no tiles); MapLibre later only if a cream-branded basemap becomes a must.
- Gotchas: multiple HKWorkoutRoute segments per workout (iterate all); headline distance = HKWorkout statistic, not route-summed.

### YOU — the RPG layer (synthesized model)

```
                 ┌───────────────────────────────┐
                 │        ⟡  AVATAR  Lv 23       │
                 │     rank C · "Consistent"     │
                 │      ◜───── XP ring ─────◝    │
                 │                               │
                 │   STR ▓▓▓▓░░   STA ▓▓▓▓▓▓░    │
                 │   AGI ▓▓░░░░   VIT ▓▓▓▓░░     │
                 │                               │
                 │  🔥 streak 6 wk   ❄ freeze ×2 │
                 └───────────────────────────────┘
```

- **XP** (one currency, no coins/gems): `XP_day = Σ(zone-minutes × mult[Z1:1, Z2:1.5, Z3:2, Z4:3, Z5:4]) + steps/1000 + activeKcal/50`, soft cap ~300/day (25% rate beyond) → consistency beats binging.
- **Levels**: Habitica quadratic `XP_next(L) = round₁₀(0.25L² + 10L + 140)` → level every 1–2 days early, ~weekly at L50. New level bars start ~12% filled (endowed-progress effect).
- **Stats**: STR ← strength workouts; STA ← steady Z2–Z3 cardio + VO2max uptrend; AGI ← HIIT/sports/Z4–Z5; VIT ← sleep in range, HRV trend, RHR downtrend, honored rest days. Radar chart = the "status window".
- **Ranks**: E→D→C→B→A→S over level bands; avatar evolutions at 5/10/20/35/50/75 (Finch life-stage pattern, drip-fed Ring-Fit-style).
- **Streak**: WEEKLY and forgiving — week kept if self-set frequency met (e.g. 4 active days); rest days count when readiness is low; auto-earn 1 freeze per kept week (cap 2, Duolingo: freezes cut churn 21%). Streak loss NEVER touches XP/levels/cosmetics.
- **XP is permanent & additive-only** — failure = no gain, never loss (abstinence-violation research). Recomputable from HealthKit history (survives reinstalls).
- Celebration within 1s of workout save (Fogg); ~1-in-5 variable cosmetic drop after workouts, never for app opens. Cap total mechanics at ~5 (S-curve research: moderate beats feature-stuffed).
- Manual RPE 1–10 after workouts written back as workoutEffortScore (feeds Apple's own Training Load too).

### Recovery/readiness (feeds Today + VIT + streak forgiveness)
`score 0–100 = sigmoid( 0.60·z(overnight HRV SDNN) − 0.25·z(sleeping RHR) + 0.15·(sleep + wrist-temp deviation) )` vs 30–60-day EWMA baseline, sampled only during the sleep window (the single biggest accuracy lever). Bands not naked numbers, Gentler-Streak framing ("ready to push" / "ease off"), manual Sick/Rest status override. Strain counterpart: Banister TRIMP on a 0–21 log scale if wanted. Don't depend on SpO2 (US legal saga; works now but treat as optional). Apple's Sleep Score has no API → recompute (duration 50 / consistency 30 / interruptions 20). Elevate **sleep regularity** (predicts mortality better than duration) and rolling 14-day **sleep debt**.

---

## 5. Architecture

```
┌────────────────────────── iPHONE APP (SwiftUI, iOS 17+) ─────────────────────────┐
│                                                                                  │
│  Views (MV pattern: @Observable stores via .environment, no MVVM ceremony)       │
│  ┌───────┐ ┌───────┐ ┌───────┐ ┌───────┐ ┌───────┐                               │
│  │ Today │ │ Train │ │ Fuel  │ │ Map   │ │ You   │   Swift Charts everywhere     │
│  └───┬───┘ └───┬───┘ └───┬───┘ └───┬───┘ └───┬───┘                               │
│      └─────────┴─────┬───┴─────────┴─────────┘                                   │
│              ┌───────▼────────┐  ┌──────────────┐  ┌──────────────┐              │
│              │  Stores        │  │ GameEngine   │  │ DesignSystem │              │
│              │  Health/Food/  │  │ XP·levels·   │  │ tokens·cards │              │
│              │  Game/Sync     │  │ streaks·TRIMP│  │ ·charts      │              │
│              └───────┬────────┘  │ (pure, tested)│ └──────────────┘              │
│                      │           └──────────────┘                                │
│   ┌──────────────────▼──────────────────┐   ┌─────────────────────────┐          │
│   │ SyncEngine (actor)                  │   │ SwiftData (app-native)  │          │
│   │ HKObserverQuery + background        │   │ FoodEntry·MealTemplate· │          │
│   │ delivery → anchored delta queries   │   │ XPEvent·AvatarState·    │          │
│   │ → persist HKQueryAnchor             │   │ Goals·RouteCache·       │          │
│   │ + FULL RESYNC ON FOREGROUND         │   │ DailyAggregate cache    │          │
│   │ (background delivery is flaky)      │   └─────────────────────────┘          │
│   └──────────────────┬──────────────────┘                                        │
│                      ▼                                                           │
│        ┌─────────────────────────┐    ┌──────────────────────────────┐           │
│        │ HealthKit = database    │    │ App Group JSON snapshot      │           │
│        │ (workouts·routes·sleep· │    │ → Widgets · Live Activity ·  │           │
│        │  HR·HRV·dietary writes) │    │   StandBy (never query HK    │           │
│        └─────────────────────────┘    │   from widgets — locked-     │           │
│                                       │   device failure)            │           │
└───────────────────────────────────────┴──────────────────────────────┴───────────┘
     ▲ auto sync                              external APIs (cached locally):
  Apple Watch Ultra 3                         USDA FoodData Central · Open Food Facts
  (Apple's Workout app records everything)
```

**Key decisions (each backed by a region):**
- **v1 is iPhone-only.** The built-in Workout app records everything (GPS routes, per-sec HR, running dynamics, effort scores) — a custom watch app adds only live in-workout UI and costs 2 of the 3 free-provisioning app slots + weekly double re-deploys. Defer to v2.
- **Read stats via HKStatisticsCollectionQuery** (auto-dedupes iPhone+Watch overlap — never sum raw samples).
- **Observer + anchored query pair** for sync; always call the observer completionHandler (3 misses = suspended); steps/energy background delivery capped hourly → foreground refresh is the primary path.
- **Never write active/basal energy to HealthKit** (documented double-count corruption).
- Live Activity during workouts works free-tier with LOCAL updates (location background mode provides runtime); auto-appears on the Watch Smart Stack via iOS 18 — workout glanceability without a watch app.
- App Intents give Ultra 3 **Action button** + Shortcuts + Control Center + interactive widgets from one implementation, all free-tier.
- Backup: HealthKit rebuilds history from nil anchor after any reinstall; app-native state exports as one JSON via fileExporter to iCloud Drive (no entitlement needed).
- Testing: pure logic (XP, TRIMP, TDEE, recovery) in dependency-free structs; HKHealthStore behind a protocol; debug seed-data screen for the simulator (sim HealthKit store is empty).

---

## 6. Design system — "Claude wearing running shoes"

Anthropic's real tokens (verified from live CSS + brand guidelines):

| Token | Light | Dark |
|---|---|---|
| Canvas | `#FAF9F5` | `#181715` (warm near-black, never pure) |
| Grouped bg | `#F0EEE6` ("ivory-medium") | `#1F1E1B` |
| Card | `#FFFFFF` / `#F5F0E8` | `#252320` |
| Ink / text | `#141413` | `#FAF9F5` |
| Hairline | `#E6DFD8` | white @8% |
| **Accent (scarce!)** | coral/clay `#D97757` | same (passes contrast) |

Domain accents (Anthropic's muted-accent logic): workouts **coral #D97757** · sleep **sky #6A9BCC** · recovery **olive #788C5D** · nutrition **amber #E8A55A**.

Rules: serif display (`.fontDesign(.serif)`, weight 400, tight tracking) + SF Pro UI + `.monospacedDigit()` for tickers · **no drop shadows** — depth via surface steps + hairlines ("color-block first") · 12pt continuous-corner cards, 20pt margins, 4/8/12/16/24/32/48 spacing · exactly ONE dark feature card per screen · coral = one CTA or one hero number per screen · Gyroscope pattern: one metric per card + 40–56pt axis-less sparkline · never imitate Apple's 3 rings (HIG violation) — one coral XP arc instead · non-judgmental copy everywhere ("Below typical Thursday", never "underperforming") · no red failure states — over-budget is just information.

**Avatar art direction** (no illustration skills needed): flat geometric SwiftUI-native mascot (Canvas/Shapes, 3–6 primitives) in brand tokens, life-stage silhouette evolutions; spring animations for level-ups. Plan B: DiceBear (Adventurer/Pixel Art styles, seed-deterministic SVGs baked to asset catalog) or CC0 Kenney sprites with `.interpolation(.none)`. Badge glyphs from game-icons.net (CC-BY). SF Symbols (hierarchical, coral-tinted) for ALL functional icons; `.symbolEffect(.bounce)` + `.sensoryFeedback(.success)` for rewards. No raw emoji in core UI (clashes with flat palette); OpenMoji outline set if emoji-like glyphs needed.

---

## 7. Build roadmap

```
Phase 0 · SETUP        Xcode 26 + Developer Mode + bundle ID forever
   │                   HealthKit auth (~45 read types, 1 sheet) · design tokens
   ▼
Phase 1 · READ         Today dashboard: rings·steps·kcal·sleep·HR cards
   │  (the wow week)   SyncEngine + resync-on-foreground · Swift Charts sparklines
   ▼
Phase 2 · TRAIN+MAP    workout feed + detail (gradient polyline·splits·elevation)
   │                   Best Efforts · TRIMP → CTL/ATL/TSB · route cache · heatmap
   ▼
Phase 3 · FUEL         food log (recents·barcode·quick-add) · USDA/OFF cached
   │                   adaptive TDEE · trend-first energy screen
   ▼
Phase 4 · RPG          GameEngine (XP·levels·stats·weekly streak·freezes)
   │                   avatar + evolutions · badges · recovery score → VIT
   ▼
Phase 5 · GLANCE       systemSmall widget (avatar+rings) · Lock Screen accessory
   │                   workout Live Activity (local updates) · App Intents/Action button
   ▼
Phase 6 · OPTIONAL     custom watchOS live-workout app (mirroring) · WorkoutKit
                       intervals · $99 upgrade decision (kills weekly re-sign)
```

Each phase ships a usable app. Phase 1 alone already beats "open 4 apps to see your day."

---

## 8. Verify-on-device list (the few uncertainties)

1. `healthkit.background-delivery` entitlement on a free personal team — probably works, no authoritative confirmation → prototype early; fallback = foreground refresh (needed anyway).
2. Watch-app slot accounting against the 3-app free limit (only matters in Phase 6).
3. SpO2 pipeline state on US Ultra 3 (re-enabled Aug 2025 via iPhone-side processing — samples land in HealthKit; keep optional).
4. Widget blank-out during the 7-day expiry window — cosmetic, but set expectations.
