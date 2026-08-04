# Product Plan — "Ember" (working name)

> A personal, all-in-one fitness RPG for one user with an iPhone + Apple Watch Ultra 3.
> Sideloaded, local-only, subscription-free, styled in Claude's cream/coral minimalism.

## 1. Vision

One calm screen that answers *"how am I doing?"* — training, food, sleep, recovery —
and one avatar that levels up only from **sensor-verified** effort. The app is an
interpretation layer over HealthKit: Apple's Watch records; Ember explains, connects,
and rewards.

**Name ideas** (pick later, cosmetic): Ember 🔥 (coral, warm, minimal — default),
Forge, Dojo, Atlas, Ronin. Bundle ID gets fixed forever at project creation, so decide
before Phase 0 — the *display* name can change anytime.

## 2. Product principles (each traced to research)

1. **Read, don't record** — Apple's Workout app is unbeatable; we consume its data. *(19-apple-native)*
2. **Trend is the truth** — daily calorie/energy numbers are estimates (+28% Watch bias); hero numbers are rolling trends. *(11-energy-models)*
3. **Additive-only progression** — XP/levels never decrease; failure = no gain, never loss. *(15-streaks, 07-gamified)*
4. **Adherence-neutral** — no red numbers, no failure states, rest is productive. *(06-nutrition, 13-ui)*
5. **Coral is scarce** — one accent moment per screen; ≤5 numbers on first screen. *(13-ui-minimal)*
6. **Everything recomputable** — XP, streaks, scores derive from HealthKit history so reinstalls cost nothing. *(17-privacy-storage)*
7. **≤5 gamification mechanics** — XP, levels, stats, weekly streak, avatar. No coins, no leaderboards, no login bonuses. *(07, 15)*

## 3. The user story map

```
 AS THE OWNER I WANT TO...                                  TAB      PHASE
 ──────────────────────────────────────────────────────     ─────    ─────
 see my whole day in one glance (rings·kcal·sleep·ready)    Today    1
 see every workout with map, splits, HR, elevation          Train    2
 know if I'm getting fitter (Fitness/Fatigue/Form, PRs)     Train    2
 see all my routes + a lifetime heatmap                     Map      2
 log food in <15 seconds and trust the target               Fuel     3
 know how hard to train today (readiness 0-100)             Today    4
 watch my avatar level up from real effort                  You      4
 keep a streak without fearing it                           You      4
 glance from home screen / during workouts                  widgets  5
 (later) live workout screen on the watch itself            watch    6
```

## 4. Scope — MoSCoW for v1 (Phases 0–4)

**MUST** — unified Today dashboard · workout feed + detail (map, splits, charts) ·
Best Efforts + PR medals · TRIMP → CTL/ATL/TSB · route cache + lifetime heatmap ·
food logging (recents/quick-add/barcode) + adaptive TDEE · recovery score 0–100 ·
XP/levels/stats/avatar/weekly streak · JSON backup export.

**SHOULD** (Phase 5) — systemSmall widget (avatar + rings) · Lock-Screen accessory
widget · workout Live Activity (local updates, auto-mirrors to Watch Smart Stack) ·
App Intents for Action button ("log water", "how am I doing") · GPX export.

**COULD** (Phase 6) — custom watchOS live-workout app (mirroring) · WorkoutKit
interval builder pushed into Apple's Workout app · sleep-debt coaching · MapLibre
cream-branded basemap · natural-language food entry via Claude API.

**WON'T** — social/feed anything · photo-AI food logging (70% accuracy, not worth it) ·
push notifications/CloudKit (impossible on free tier) · own workout recorder on iPhone ·
ACWR "injury risk" warnings (science doesn't support) · Apple-style triple rings (HIG).

## 5. Open decisions (defaults chosen, veto anytime)

| # | Decision | Default | Why |
|---|---|---|---|
| D1 | Mac access | **Required** — need Xcode on macOS to build/deploy | No workaround exists; cloud Macs (MacStadium etc.) possible if no Mac |
| D2 | Free vs $99/yr | **Start free**, revisit after 4–6 weekly re-signs | Everything in v1 works free; $99 buys convenience (1-yr profile, TestFlight) not features |
| D3 | v1 watch app | **No** — iPhone-only | Watch app costs 2 of 3 free app slots + double re-signs; Apple's Workout app records everything already |
| D4 | Avatar art | **Procedural SwiftUI mascot** (flat geometric, brand tokens) | Zero assets, animatable; DiceBear/Kenney sprites as plan B if it doesn't feel "anime" enough |
| D5 | Code repo | **New dedicated repo** (`ember-ios` or similar) | Keep docs repo clean; add via add_repo when created |
| D6 | Min iOS | **iOS 18** | User has current hardware; unlocks workoutEffortScore, modern APIs, no legacy paths |
| D7 | Local DB | **SwiftData** | App-native state is tiny (HealthKit holds the heavy data); GRDB only if pain appears |

## 6. Success criteria

- **Week 1**: dashboard replaces opening Fitness + Health apps each morning.
- **Month 1**: all five tabs live; food logged daily <15s; readiness trusted enough to plan workouts.
- **Month 2**: avatar level feels *earned*; weekly streak survives a rest week without anxiety.
- **Forever**: $0/yr; no data leaves the phone; weekly re-sign stays a <5-min ritual.

## 7. Risks

| Risk | Likelihood | Mitigation |
|---|---|---|
| HealthKit background delivery blocked on free team | medium | Foreground-refresh is the primary path anyway; verify in Phase 0 spike |
| Weekly re-sign fatigue | medium | Wi-Fi deploy ritual; $99 escape hatch (D2) |
| SwiftData migration bugs | low-med | Models kept flat/simple; JSON export as safety net |
| Scope creep (the all-in-one trap) | high | MoSCoW above is the contract; every phase ships usable |
| Watch/phone double-count bugs | med | HKStatisticsCollectionQuery only; never sum raw samples |
