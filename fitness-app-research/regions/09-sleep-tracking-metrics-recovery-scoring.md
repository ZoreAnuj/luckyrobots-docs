# Sleep tracking, metrics & recovery scoring

## Key findings
- Apple Watch natively records four sleep states — Awake, REM, Core (light/N1+N2), and Deep (N3) — using accelerometer motion plus heart rate; stage estimation was validated against polysomnography in Apple's own published paper (updated Oct 2025). All of this flows into HealthKit for free, so a personal app never needs its own stage-detection engine.
- watchOS 26 (2025) added a native Sleep Score (0-100) computed from three weighted components: Duration (up to 50 pts), Bedtime consistency (up to 30 pts), and Interruptions/wake-ups (up to 20 pts). It is built on 5M+ nights from Apple's Heart & Movement Study. iOS/watchOS 26.2 later re-tuned the score ranges/classifications. This score is readable from HealthKit, so the app can surface Apple's number rather than reinventing it.
- During sleep the Watch also passively records wrist temperature (appleSleepingWristTemperature, a nightly deviation baseline), respiratory rate (breaths/min), blood oxygen (SpO2), heart rate, and a Breathing Disturbances metric — all queryable via HealthKit as standard samples.
- Sleep apnea notifications work by accumulating the 'Breathing Disturbances' metric (nightly elevated/not-elevated) over a rolling 30-day window; consistent moderate-to-severe elevation triggers a possible-apnea alert plus an exportable PDF. It is FDA-cleared/available on Series 9+, Ultra 2+, and the notification logic runs on-device, not in third-party apps.
- The single most important research finding for this region: sleep REGULARITY (consistency of sleep/wake timing) is a STRONGER predictor of all-cause mortality than sleep DURATION. UK Biobank study (n=60,977, Sleep 2024) found the most-regular quintiles had 20-48% lower all-cause mortality vs the least regular. A minimal app should elevate a consistency/regularity metric to top-billing, not just total hours.
- Sleep debt is the cumulative deficit vs personal need, tracked by sleep scientists on a rolling ~14-day window; it has no ceiling and cannot be fully repaid in one weekend (Current Biology 2021: recovery sleep restored attention/reaction time but not complex executive function). This is the metric RISE built its whole app around and the one users find most actionable.
- The two dominant recovery/readiness models diverge sharply: WHOOP Recovery is HRV-dominated (HRV alone explains ~56% of score variance), while Oura Readiness is multi-dimensional (HRV <5% of variance) blending 7 contributors including resting HR, HRV balance, body temp deviation, recovery index, sleep score, sleep balance/debt, and prior-day activity. For a personal app, the Oura-style multi-input blend is more robust and less jittery than a pure-HRV score.
- Third-party sleep apps split into 'data vs guidance': AutoSleep ($7.99 one-off) and Pillow give exhaustive raw data using Watch sensors; RISE ($69.99/yr) and Sleep Cycle ($39.99/yr) give simpler metrics with actionable coaching. RISE's differentiator is accurate sleep-debt calc + circadian alignment; AutoSleep's sleep-debt is only vs a user-set goal (less accurate). Sleep Cycle/Pillow can use the mic for snore detection but Watch-based staging is more accurate.

## Specifics
- HealthKit category type: HKCategoryTypeIdentifier.sleepAnalysis with HKCategoryValueSleepAnalysis enum: .inBed, .asleepUnspecified, .awake, .asleepCore, .asleepREM, .asleepDeep (last three added iOS 16/watchOS 9, 2022).
- Query predicate: use HKCategoryValueSleepAnalysis.allAsleepValues (a Set) to fetch all asleep stages including unspecified; there is also .predicateForCategorySamples. Store one sample per continuous period in a given stage.
- HKQuantityTypeIdentifier.appleSleepingWristTemperature — nightly wrist temp during sleep, expressed as a deviation from a personal baseline (degC), one sample per night.
- HKQuantityTypeIdentifier.respiratoryRate — breaths/min, auto-recorded on Apple Watch during sleep.
- HKQuantityTypeIdentifier.oxygenSaturation — blood oxygen % (SpO2), sampled during sleep on supported Watches.
- HKQuantityTypeIdentifier.appleSleepingBreathingDisturbances — the apnea-detection metric; nightly elevated/not-elevated; viewable over 1 month / 6 months / 1 year; drives the 30-day apnea notification.
- Apple Sleep Score components & weights (watchOS 26): Duration up to 50 pts, Bedtime consistency up to 30 pts, Interruptions/wake-ups up to 20 pts; total 0-100. Score classification bands revised in iOS/watchOS 26.2.
- Sleep Efficiency formula: (total time asleep / total time in bed) x 100. Normal >= 85%; healthy young adults often >90%; <80% flagged. Easy to compute from HealthKit inBed vs asleep samples.
- Sleep Regularity Index (SRI) formula: SRI = -100 + (200 / (M(N-1))) * sum over days & epochs of delta(s_i,j, s_i+1,j), where delta=1 if the sleep/wake state matches at time-points 24h apart. Range -100 to 100 (100=perfectly regular, 0=random). Reference impl: github.com/ejain/sleep-regularity-index and R package GGIR CalcSleepRegularityIndex.
- Normal adult sleep-stage distribution to benchmark against: N1 ~3-8%, N2 ~45-55%, N3/deep ~13-23% (~1-2h), REM ~20-25% (~90-120min). Deep concentrates in first half of night, REM in second half.
- Apple Watch maps its 3 stages to PSG: Core = N1+N2, Deep = N3, plus REM. Average Apple Watch user gets ~49 min deep sleep/night (Empirical Health analysis).
- Sleep debt: track on rolling 14-day window; rule of thumb, debt creeping past ~5h is worth actively paying down. UPenn dose-response study: 6h/night for 14 days = deficit equal to two full sleepless nights. Subjective sleepiness stabilizes after 2-3 days even as cognitive decline continues (people underestimate their own impairment).
- WHOOP Recovery inputs: HRV (dominant, ~56% variance), resting HR, respiratory rate, sleep performance, skin temp, SpO2 — all captured during sleep.
- Oura Readiness inputs (7): resting HR, HRV balance, body temp deviation, recovery index (how fast RHR stabilizes overnight), sleep score, sleep balance (recent sleep debt), previous-day activity; HRV <5% of variance.
- Chronotype = circadian preference (morning/evening 'lark vs owl'); derivable from midpoint of sleep on free days (MSF) a la Munich Chronotype Questionnaire; RISE and circadian apps use it to time light exposure and predict energy peaks/dips.
- App pricing benchmarks: AutoSleep $7.99 one-off; Sleep Cycle $39.99/yr; RISE $69.99/yr; Oura & WHOOP require hardware + subscription.

## Recommendations
- Consume everything from HealthKit rather than building detection: read sleepAnalysis stage samples, the native watchOS 26 Sleep Score, appleSleepingWristTemperature, respiratoryRate, oxygenSaturation, heart rate, and appleSleepingBreathingDisturbances. Zero sensor code needed — the Watch Ultra 3 supplies it all for free.
- Make a sleep CONSISTENCY/regularity metric a first-class citizen on the sleep screen (compute SRI or a simpler bedtime-variance ring), because the research says it beats duration for health outcomes — and it fits the minimal Anthropic aesthetic better than a wall of stage charts.
- Compute and headline a rolling 14-day sleep-debt number (asleep hours vs a personal need target) — it is the single most actionable, behavior-changing metric per RISE/WHOOP, and it feeds naturally into the RPG layer (sleep debt = a 'stamina/HP' drain on the avatar).
- For the recovery/readiness score, use an Oura-style multi-input blend (resting HR trend, HRV, wrist-temp deviation, respiratory rate, prior sleep + sleep debt) rather than a WHOOP-style pure-HRV score — it is more stable night-to-night and less prone to alarming single-night swings, which suits a calm minimal app.
- Show sleep-stage percentages against the published normal bands (deep 13-23%, REM 20-25%) as gentle 'in range' indicators rather than raw hypnograms, to keep the UI clean and give meaning instead of just data.
- Surface Apple's native Sleep Score and its three-part breakdown (duration/consistency/interruptions) directly so users get a trusted number, then layer your own sleep-debt and readiness scores as the app's distinctive value-add.
- Respect the apnea metric's medical framing: display Breathing Disturbances trends read-only and defer to Apple's own 30-day notification; never present your own apnea 'diagnosis' — it is a regulated space and the Watch already handles it on-device.
- Design a single warm 'last night' card (score ring, hours, consistency, readiness) as the default sleep view with progressive disclosure to stages/temp/respiratory on tap — matching the generous-whitespace, one-glance Claude-branding aesthetic and echoing AutoSleep's clarity without its data overload.

## Sources
- https://developer.apple.com/documentation/healthkit/hkcategoryvaluesleepanalysis
- https://www.apple.com/health/pdf/Estimating_Sleep_Stages_from_Apple_Watch_Oct_2025.pdf
- https://appleinsider.com/articles/25/09/12/how-sleep-score-works-on-apple-watch-with-watchos-26
- https://academic.oup.com/sleep/article/47/1/zsad253/7280269
- https://www.whoop.com/us/en/thelocker/how-does-whoop-recovery-work-101/
- https://www.sensai.fit/blog/garmin-body-battery-vs-whoop-recovery-vs-oura-readiness-how-calculated-2026
- https://www.apple.com/health/pdf/sleep-apnea/Sleep_Apnea_Notifications_on_Apple_Watch_September_2024.pdf
- https://www.risescience.com/blog/best-sleep-debt-tracking-app
