# Implementation Plan — Ember

Technical design + build workflow. Companion to [PRODUCT-PLAN.md](PRODUCT-PLAN.md)
and [SYNTHESIS.md](SYNTHESIS.md).

---

## 1. The development workflow (who does what)

Claude runs on Linux; iOS builds need a Mac. The loop is:

```
        CLAUDE (this session / repo)                YOU (Mac + iPhone + Watch)
 ┌─────────────────────────────────────┐      ┌──────────────────────────────────┐
 │ 1 write Swift code, tests, specs    │      │ 3 git pull → open in Xcode       │
 │ 2 run pure-logic tests on Linux     │─────▶│ 4 ⌘R → deploys to iPhone         │
 │   (GameEngine/Math are portable     │ push │ 5 tap around, screenshot,        │
 │    Swift packages — testable here)  │      │   paste errors/screenshots back  │
 │ 6 fix / iterate from your feedback  │◀─────│   (Xcode errors are copy-paste-  │
 └─────────────────────────────────────┘ chat │    able text)                    │
                                              └──────────────────────────────────┘
        weekly ritual (2 min): plug in OR Wi-Fi → ⌘R re-sign before day 7
```

Rules of the loop:
- **Every PR/commit builds** — I never push code that references APIs I'm unsure of without flagging it `// VERIFY:` for your first build.
- **Pure logic is Linux-tested**: `GameEngine`, `TrainingMath`, `NutritionMath` have zero Apple-framework imports → I run `swift test` here before pushing.
- **Device-only surfaces** (HealthKit auth, background delivery, widgets, Live Activity) get a written manual test checklist per phase; you run it once per phase.
- **Simulator seeding**: a debug-only screen fabricates workouts/sleep/HR via HKWorkoutBuilder so UI work doesn't need the real watch every time.

## 2. Project structure

New repo, one Xcode project + one local Swift package:

```
ember-ios/
├── Ember.xcodeproj
├── Ember/                          # app target (iOS 18+)
│   ├── EmberApp.swift              # @main, store injection, scenePhase resync
│   ├── Features/
│   │   ├── Today/                  # dashboard cards
│   │   ├── Train/                  # feed, WorkoutDetail, load charts
│   │   ├── Fuel/                   # logger, food search, energy screen
│   │   ├── Map/                    # heatmap, route browser
│   │   ├── You/                    # avatar, stats, badges, streak
│   │   ├── Onboarding/             # permission explainer → HK auth sheet
│   │   └── Debug/                  # simulator seed-data screen (DEBUG only)
│   └── Resources/                  # Assets.xcassets, fonts (Lora optional)
├── EmberWidgets/                   # widget extension target (Phase 5)
│   ├── TodayWidget.swift           # systemSmall: avatar + rings
│   ├── AccessoryWidgets.swift      # lock screen circular/rectangular
│   └── WorkoutLiveActivity.swift
└── FitnessKit/                     # local SPM package
    ├── Sources/
    │   ├── Models/                 # pure domain structs, ZERO imports
    │   ├── GameEngine/             # XP, levels, stats, streaks   ← Linux-testable
    │   ├── TrainingMath/           # TRIMP, CTL/ATL/TSB, zones,
    │   │                           #   best efforts, recovery     ← Linux-testable
    │   ├── NutritionMath/          # adaptive TDEE, BMR, trends   ← Linux-testable
    │   ├── HealthService/          # HKHealthStore wrapper + SyncEngine actor
    │   ├── Persistence/            # SwiftData models, RouteCache, snapshots
    │   ├── FoodAPI/                # USDA + Open Food Facts clients + cache
    │   └── DesignSystem/           # tokens, cards, chart styles, haptics
    └── Tests/                      # Swift Testing; fixtures for every formula
```

App IDs consumed (10/week cap!): `com.<you>.ember` + `com.<you>.ember.widgets` = 2. Fixed forever.

## 3. Data model

**HealthKit = system of record** (read: ~45 types; write: dietary*, bodyMass,
workoutEffortScore). **SwiftData = app-native only:**

```
@Model FoodItem       name, brand, per-100g + perServing macros, barcode?, source(usda|off|custom), lastUsedServing
@Model FoodEntry      date, meal(breakfast|lunch|dinner|snack), item→FoodItem?, quickAdd?, kcal, p/c/f, hkSyncID
@Model MealTemplate   name, [FoodItem+qty]                     # "my usual breakfast"
@Model XPEvent        date, source(workout|steps|energy|sleep|streak|bonus), amount, workoutUUID?   # append-only ledger
@Model AvatarState    level, totalXP, statXP{str,sta,agi,vit}, rank, evolutionTier, cosmetics[]
@Model StreakState    weekStart, daysKept, freezesBanked, longestWeeks, status(active|sick|rest|travel)
@Model GoalSettings   weeklyActiveDays, calorieTarget?, macroSplit, maxHR?, LTHR?, hrZoneModel
@Model RouteCache     workoutUUID, simplifiedCoords(blob), fullCoords(blob), perPointSpeed/alt, thumbnailPNG, dark/light
@Model DailyAggregate date, steps, activeKcal, basalKcal, exerciseMin, sleepSec, hrv, rhr, trimp, recoveryScore, xp
                      # cache for instant charts; recomputable from HealthKit

UserDefaults/App Group:  HKQueryAnchor per type (archived) · DailySnapshot JSON for widgets
```

`DailySnapshot` (App Group, ≤4KB, feeds every widget/Live Activity — they never touch HealthKit):
`{date, ringsPct, activeKcal, consumedKcal, sleepHours, recovery, level, xpToday, xpPct, streakWeeks, avatarTier, updatedAt}`

## 4. Engine specs (the Linux-testable core)

### GameEngine
```
XP_day      = Σ_workouts Σ_zones (min_z × mult_z)  + steps/1000 + activeKcal/50
              mult = [Z1:1, Z2:1.5, Z3:2, Z4:3, Z5:4]
              soft cap: XP beyond 300/day earns at 25%
XP_next(L)  = round_to_10(0.25L² + 10L + 140)           # Habitica curve
ranks       E:1-9 D:10-19 C:20-34 B:35-49 A:50-74 S:75+ # evolutions at 5/10/20/35/50/75
stats       STR←strength/functional/core min · STA←Z2-Z3 cardio min (+VO2max uptrend bonus)
            AGI←HIIT/sports/Z4-Z5 min · VIT←sleep-in-range + HRV↑ + RHR↓ + honored rest
streak      week kept if activeDays ≥ goal (rest day counts when recovery<40 or status set)
            +1 freeze per kept week (max 2); auto-consumed; loss never touches XP
invariants  XP monotonic non-decreasing · full ledger replayable from HealthKit history
```

### TrainingMath
```
zones(HRR)  Karvonen: HR_target = RHR + frac×(maxHR−RHR); maxHR default Tanaka 208−0.7·age
TRIMP       Edwards: Σ min_in_zone × zone_index(1..5)          # per workout
CTL_t       = CTL_{t-1} + (load_t − CTL_{t-1})/42               # Fitness
ATL_t       = ATL_{t-1} + (load_t − ATL_{t-1})/7                # Fatigue
TSB_t       = CTL_{t-1} − ATL_{t-1}                             # Form
bestEfforts fastest elapsed-time window per distance ∈ {1K,1mi,5K,10K,half} per run
            (two-pointer over cumulative route/time series); top-3 lifetime, top-10 year
recovery    z_hrv = (HRV_night − μ60d)/σ60d ; z_rhr likewise (inverted)
            raw = 0.60·z_hrv − 0.25·z_rhr + 0.15·(sleepFactor + tempDeviation)
            score = 100·sigmoid(raw); bands: ready≥67 · steady 34-66 · easeOff<34
            needs ≥3 nights history; sample HRV/RHR only inside sleep window
sleep       efficiency = asleep/inBed · consistency = σ(bedtime, 14d) mapped to 0-100
            debt = Σ_14d (need − slept)
```

### NutritionMath
```
seed TDEE   Mifflin-St Jeor (±sex constant) × activity 1.2-1.9 (first ~2 weeks only)
trend       weight_t = EWMA(dailyWeighIns, α≈0.1)
adaptive    TDEE = mean_14-28d(intake) − slope(weight_trend) kg/day × 7700
target      TDEE + goalRate_kg_wk × 7700/7 ; never add exercise kcal back
display     weekly averages + bands; single-day always labeled estimate
```

## 5. SyncEngine (the one hard component)

```
                    ┌─ app launch / scenePhase→.active ────────────┐
                    ▼                                              │
   ┌── for each tracked type ──────────────────────────────┐       │
   │ load HKQueryAnchor → HKAnchoredObjectQueryDescriptor  │  full resync path
   │ → apply delta (new samples + HKDeletedObject)         │  (PRIMARY, self-healing)
   │ → recompute DailyAggregates touched → append XPEvents │       │
   │ → persist new anchor → write DailySnapshot            │       │
   │ → WidgetCenter.reloadAllTimelines()                   │       │
   └───────────────────────────────────────────────────────┘       │
                    ▲                                              │
   HKObserverQuery + enableBackgroundDelivery(.immediate for      │
   workouts/HR, hourly floor for steps/energy) — OPPORTUNISTIC     │
   bonus only; ALWAYS call completionHandler (3 misses = dead) ────┘
```

- Stats via `HKStatisticsCollectionQuery` day-buckets (auto watch/phone dedupe).
- First run (or post-reinstall): nil anchor → full history import → XP ledger replayed
  deterministically → avatar restored without any backup.
- Route pipeline: on new workout → fetch ALL HKWorkoutRoute segments → concat →
  Douglas-Peucker simplify → RouteCache + MKMapSnapshotter thumbnail (light+dark PNG).

## 6. Screen blueprints

```
 TODAY                          TRAIN › detail                 YOU
┌──────────────────┐   ┌────────────────────────┐   ┌──────────────────┐
│ Tue, Aug 4   ☀︎   │   │ ◀ Morning Run    🏅PR  │   │      ⟡           │
│                  │   │ ┌────────────────────┐ │   │   (avatar,       │
│ ┌──────────────┐ │   │ │  gradient polyline │ │   │    tier 3)       │
│ │ MOVE  486/600│ │   │ │  map (pace-colored)│ │   │  ◜─ xp ring ─◝   │
│ │ ▓▓▓▓▓▓▓▓░░   │ │   │ └────────────────────┘ │   │  Lv 23 · rank C  │
│ └──────────────┘ │   │ 5.21km  26:14  5:02/km │   │  1,240 / 1,910   │
│ 😴 7:12  ● 82    │   │ ♥161avg  ▲48m  312kcal │   │                  │
│ ❤ 52    ready ●  │   │ ── splits ──────────── │   │ STR ▓▓▓▓░ STA ▓▓▓▓▓▓│
│ 🔥 1,846 / 2,410 │   │ 1  4:58 ▓▓▓▓▓▓▓ ♥158   │   │ AGI ▓▓░░░ VIT ▓▓▓▓░ │
│ ⚔ +140 XP  ▓▓▓░  │   │ 2  5:04 ▓▓▓▓▓▓  ♥162   │   │                  │
│                  │   │ [pace/HR chart over    │   │ streak 6 wk ❄×2  │
│ (dark card: next │   │  shaded elevation,     │   │ ┌─ badges ─────┐ │
│  quest / workout)│   │  drag-to-scrub ↔ map]  │   │ │ ◉ ◉ ◉ ○ ○    │ │
└──────────────────┘   │ effort RPE [1-10] tap  │   │ └──────────────┘ │
                       └────────────────────────┘   └──────────────────┘
 FUEL                                    MAP
┌──────────────────┐   ┌────────────────────────┐
│ target 2,140     │   │ [lifetime heatmap —    │
│ eaten 1,415      │   │  all routes, coral     │
│ ▓▓▓▓▓▓▓░░░ 66%   │   │  glow on muted map]    │
│ P 92g C 143 F 48 │   │                        │
│ ── recents ────  │   │ ── this month ──────── │
│ ⊕ oats+banana    │   │ ▢ route ▢ route ▢ route│
│ ⊕ chicken bowl   │   │ (thumbnail grid,       │
│ [scan] [search]  │   │  tap → Train detail)   │
│ [copy yesterday] │   └────────────────────────┘
└──────────────────┘
```

Design tokens per SYNTHESIS §6: cream/ivory surfaces, serif display numerals,
monospaced digits, hairlines not shadows, one dark card per screen, coral scarce.

## 7. Build sequence — 7 milestones, each ships usable

```
M0  SPIKE+SKELETON      project, bundle IDs, tokens, tabs, HK auth flow,
     (weekend)          ★ device spike: background-delivery entitlement on free team
M1  TODAY               SyncEngine v1 (steps·energy·sleep·HR·rings) → dashboard
     (week 1)           cards + sparklines · DailyAggregate cache · seed-data screen
M2  TRAIN read          workout feed + detail (stats grid, splits, HR/elev charts)
     (week 2)           RouteCache + thumbnails + gradient polyline detail map
M3  TRAIN math + MAP    TRIMP/CTL/ATL/TSB card · Best Efforts + PR medals ·
     (week 3)           heatmap overlay · GPX export
M4  FUEL                logger (recents→quick-add→barcode→search) · FoodAPI cache ·
     (week 4)           HealthKit dietary writes · adaptive TDEE + trend screen
M5  RPG                 GameEngine wired: XP ledger replay, avatar+evolutions,
     (week 5)           stats radar, weekly streak+freezes, recovery→VIT, RPE sheet
M6  GLANCE              App Group snapshot → systemSmall + accessory widgets ·
     (week 6)           workout Live Activity (local) · App Intents (Action button)
──  later: M7 watch app (mirroring) · WorkoutKit intervals · $99 decision
```

Definition of done per milestone: builds clean on your Mac · pure-logic tests green on
Linux · manual device checklist passed · screenshot review vs design tokens · committed.

## 8. Phase-0 verification spike (before writing much code)

Run on real iPhone, ~1 hour:
1. HealthKit capability + auth sheet on free personal team (expected ✅)
2. `enableBackgroundDelivery` + observer fire with app backgrounded (the one real unknown)
3. Read one workout + route + per-second HR from an actual Ultra 3 recording
4. Confirm App Group container works free-tier (widgets depend on it)
5. Time the Wi-Fi re-deploy ritual end-to-end

Outcomes gate the plan: if (2) fails → foreground-only sync (minor); everything else
has no plan-B needed per research.
