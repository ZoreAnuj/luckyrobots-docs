# Personal iOS Fitness App — Research

Research for a **personal, sideloaded, all-in-one iOS fitness app** for an Apple Watch
Ultra 3 owner: workouts, calories/nutrition, Strava-like route maps, sleep, recovery, and a
minimal RPG/leveling layer — styled after Anthropic/Claude's cream-and-coral aesthetic.
Never published to the App Store; installed via Xcode free provisioning.

## Contents

- **[SYNTHESIS.md](SYNTHESIS.md)** — the master document: feasibility verdict, product
  thesis, data inventory, feature spec (5 tabs), XP/RPG model, architecture, design
  system, and phased build roadmap.
- **[regions/](regions/)** — raw findings from 21 parallel research agents, one file per
  subregion:

| # | Region | Covers |
|---|---|---|
| 00 | distribution | Sideloading without the App Store: free provisioning, 7-day expiry, AltStore/TestFlight, EU vs US |
| 01 | healthkit-catalog | Every relevant HealthKit identifier, query APIs, auth model, background delivery |
| 02 | ultra3-sensors | Ultra 3 hardware, what's developer-exposed vs Apple-locked |
| 03 | watchos-live | HKWorkoutSession/live workout stack, mirroring, what you get free from Apple's Workout app |
| 04 | strava | Full Strava teardown: features, paywall history, map types, formulas |
| 05 | running-apps | NRC, Runna, Runkeeper, TrainingPeaks, Garmin, HealthFit, Footpath |
| 06 | nutrition-apps | MFP, MacroFactor, Cronometer, Lose It, Yazio, HealthifyMe + free food APIs |
| 07 | gamified-apps | Habitica/WalkScape/Finch/Gentler Streak math + a synthesized XP model |
| 08 | recovery-apps | Whoop/Oura/Athlytic/Bevel readiness formulas, all HealthKit-replicable |
| 09 | sleep | Watch sleep staging, Sleep Score, sleep debt & regularity research |
| 10 | maps-gps | HKWorkoutRoute → MapKit: gradient polylines, heatmaps, snapshots, GPX |
| 11 | energy-models | Calorie accuracy science (~28% Watch overestimate), adaptive TDEE |
| 12 | training-science | TRIMP variants, CTL/ATL/TSB, zones, VO2max validation, ACWR criticism |
| 13 | ui-minimal | Anthropic design tokens + minimal fitness-UI patterns (Gentler Streak, Gyroscope, Bevel) |
| 14 | swiftui-arch | MV + @Observable, SyncEngine pattern, SwiftData split, testing |
| 15 | streaks-psychology | Behavior-change research: streak freezes, SDT, implementation intentions |
| 16 | widgets-ecosystem | WidgetKit, Live Activities, StandBy, App Intents under free provisioning |
| 17 | privacy-storage | HealthKit as durable store, backup/export strategy for a sideloaded app |
| 18 | competitor-gaps | Ranked user pain points across the market (⚠ model-knowledge, no live web) |
| 19 | apple-native | Apple Fitness/Health/Workout teardown: lean-on vs build split (⚠ model-knowledge) |
| 20 | icons-avatar | Avatar/badge art direction + free asset sourcing for a non-illustrator |

## One-paragraph verdict

A native SwiftUI + HealthKit iPhone app, sideloaded with a free Apple ID (7-day re-sign
ritual), is fully feasible — HealthKit works on free provisioning and a web app/PWA cannot
access Watch data at all. The Watch Ultra 3 + Apple's own Workout app already record
everything (GPS routes, per-second HR, running dynamics, sleep stages, HRV, wrist temp);
the app is a read-heavy HealthKit client that adds the four things Apple doesn't ship: a
unified daily dashboard, nutrition logging with an adaptive TDEE, a Whoop-style recovery
score, and a sensor-verified RPG leveling layer — all local, private, and subscription-free.
