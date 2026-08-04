# recovery-apps

## Key findings
- Nearly every recovery/readiness app reduces to the SAME core model: compare today's overnight HRV and resting HR (RHR) against a personal rolling baseline (typically 14-60 days). HRV is always the dominant input, RHR secondary. This core is 100% replicable from Apple Watch HealthKit alone.
- WHOOP Recovery (0-100%) is built primarily from HRV, RHR, respiratory rate, and sleep performance, each compared to YOUR personal baseline (not population norms). Third-party regression analysis found HRV alone explains ~56% of the variance in the Recovery score, confirming HRV is by far the heaviest-weighted input. WHOOP measures during sleep to reduce noise.
- WHOOP Strain (0-21) is a LOGARITHMIC scale (not linear) modeled on Borg RPE (6-20). It accumulates cardiovascular load from time-in-heart-rate-zones relative to personal max HR, plus muscular load for strength sessions. Going from 16->17 is far harder than 4->5. This is essentially a rebranded TRIMP/heart-rate-reserve integral.
- Oura Readiness is an explicit weighted sum of named contributors: Resting Heart Rate, HRV Balance, Body Temperature, Recovery Index, Sleep, Sleep Balance, Previous Day Activity, and Activity Balance. 'Balance' contributors use a 14-day weighted average (last 2-5 days weighted slightly higher) compared to a ~2-month long-term baseline.
- Athlytic (iOS/Apple Watch) is the closest pure-HealthKit clone of WHOOP: Recovery (0-100%) = today's sleeping HRV and overnight RHR vs a 60-day rolling baseline of each, with HRV weighted slightly higher than RHR. 50% means you are exactly at baseline. It also computes a WHOOP-style Exertion/Strain score.
- Training Today computes a 'Readiness To Train' (RTT) score purely from Apple Health HRV: it takes the HRV 60-day average and compares it to a rolling 24h of HRV data, with workout detection and outlier filtering. Demonstrates a credible readiness score can be built from HRV alone.
- Bevel explicitly markets itself as 'WHOOP from your Apple Watch.' Recovery needs 2 full nights first; uses RMSSD HRV (converted/derived) and RHR captured only during sleep, compared to a 60-day baseline. Models the Strain -> Sleep need -> Recovery -> next-day Strain-capacity loop like WHOOP.
- Gentler Streak avoids a single readiness number; its 'Activity Path' is a moving green band (target load) vs a dotted line (actual load) derived from workout intensity/duration/frequency, RHR, and sleep from Apple Health, plus manual status (Sick/Injured/On a Break). It is a load-management / anti-overtraining framing rather than a physiology score.
- Welltory is phone-camera-PPG based (finger on flash), not wearable-first: it records RR-intervals via camera video, computes HRV, and outputs Stress and Energy scores by comparing to a personal baseline and an age/gender population database. Less relevant to an Apple-Watch app but shows the HRV->stress/energy mapping.
- Apple Watch natively records everything the core recovery model needs: HRV as SDNN (heartRateVariabilitySDNN, ~60s windows), restingHeartRate, respiratoryRate (during sleep), appleSleepingWristTemperature (Series 8+/Ultra), sleep stages (Awake/REM/Core/Deep), and workout heart rate. Blood oxygen (SpO2) is the one input that is legally disabled on newer US units.
- The academic backbone these apps rebrand is Banister's TRIMP (1975): TRIMP = duration(min) x HR-reserve-fraction x exponential weighting factor, where HRR fraction = (avgHR - restHR)/(maxHR - restHR) and the male weighting is 0.64 * e^(1.92 * HRR). This is directly implementable in Swift and reproduces WHOOP-style strain.
- The main things NOT replicable from Apple Watch are the proprietary sensor extras: Oura's ring-based finger temperature (more stable than wrist), WHOOP's continuous skin temp and SpO2, and each company's opaque ML tuning. The score's CORE (HRV+RHR+sleep vs baseline) is fully replicable; the last ~10-20% of proprietary polish is not.

## Specifics
- HealthKit HRV identifier: HKQuantityTypeIdentifier.heartRateVariabilitySDNN (unit: ms). Apple Watch uses SDNN, not RMSSD; measured opportunistically in ~60s windows during rest and roughly every ~2h overnight (and via the Breathe/Mindfulness app on demand).
- HealthKit resting HR: .restingHeartRate (bpm, one value/day). For sleeping RHR many apps instead average .heartRate samples during the sleep window.
- HealthKit respiratory rate: .respiratoryRate (breaths/min), auto-recorded during sleep on Apple Watch (watchOS 2.0+ type; sleep-derived values need watchOS 8+).
- HealthKit wrist temperature: .appleSleepingWristTemperature (deviation-style nightly temp, Apple Watch Series 8+ and Ultra). This is the Apple analog to Oura/WHOOP temperature deviation.
- HealthKit sleep: HKCategoryTypeIdentifier.sleepAnalysis with values inBed, asleepUnspecified, asleepREM, asleepCore, asleepDeep, awake. Enables sleep-stage weighting and defining the overnight window for HRV/RHR sampling.
- HealthKit blood oxygen: .oxygenSaturation (SpO2) exists but automatic Blood Oxygen measurement is disabled on US Series 9/Ultra 2 and later (Masimo patent dispute) — do NOT rely on it for a US Ultra 3 user.
- WHOOP Recovery inputs (official): HRV, resting heart rate, respiratory rate, sleep duration/quality, skin temperature, blood oxygen — each vs personal baseline. Output 0-100%, color-coded (green 67-100, yellow 34-66, red 0-33).
- WHOOP Recovery reverse-engineered: HRV explains ~56% of variance (Sportsmith analysis); original model (pre-respiratory-rate) was HRV + RHR + Sleep Performance per Capodilupo (Podcast Ep. 40).
- WHOOP Strain: 0-21 logarithmic scale patterned on Borg RPE 6-20; combines cardiovascular load (time in HR zones relative to personal max HR) + muscular load (strength). Nonlinear: each higher point is exponentially harder.
- Oura Readiness contributors (weighted composite, each 0-100): Resting Heart Rate, HRV Balance, Body Temperature, Recovery Index (hours of recovery sleep after HR hit baseline), Sleep, Sleep Balance, Previous Day Activity, Activity Balance.
- Oura 'Balance' contributors: compare recent 14-day weighted average (past 2-5 days weighted slightly higher) to ~2-month long-term average. Body temp normal range 95.9-99.3F (35.5-37.4C); scored as nightly deviation from personal baseline.
- Athlytic Recovery: 0-100% = f(today sleeping-HRV, overnight RHR) vs 60-day rolling baseline of each; HRV weighted slightly above RHR; 50% = at baseline; Sleep Prioritization ON by default uses average HRV during sleep + overnight RHR.
- Training Today RTT: score from HRV 60-day average (Apple Health) vs rolling 24h HRV; adds workout detection + outlier filtering; HRV-only readiness.
- Bevel Recovery: RMSSD HRV + RHR captured only during sleep vs 60-day baseline; requires 2 full nights before scoring; models Strain->Sleep need->Recovery->Strain-capacity loop.
- Gentler Streak Activity Path: moving green target band vs dotted actual-load line; inputs = workout intensity/duration/frequency + RHR + sleep from Apple Health + manual status (Active/On a Break/Sick/Injured). No single readiness number.
- Welltory Stress & Energy: camera-PPG RR-intervals -> HRV -> Stress and Energy scores vs personal baseline + age/gender population DB; claims Polar chest-strap-level accuracy; ML trained on ~2B data points.
- Banister TRIMP formula: TRIMP = duration(min) x HRR x weightingfactor; HRR = (avgHR - restHR)/(maxHR - restHR); male weight = 0.64 * e^(1.92*HRR), female = 0.86 * e^(1.67*HRR). Directly reproduces WHOOP-style strain in code.
- Common baseline windows observed: WHOOP/Athlytic/Bevel ~60-day rolling; Oura 14-day (balance) vs ~2-month long-term; Training Today 60-day HRV average vs 24h. A 30-60 day EWMA of overnight HRV+RHR is the practical default.
- Standard readiness normalization pattern: z-score today's overnight HRV against baseline mean/SD, do same (inverted) for RHR, weight ~0.6 HRV / ~0.25 RHR / ~0.15 sleep+temp, map to 0-100 via a sigmoid/linear clamp. Matches Athlytic/WHOOP behavior.

## Recommendations
- Build ONE recovery score from pure HealthKit: overnight HRV (heartRateVariabilitySDNN) + sleeping RHR vs a 30-60 day rolling baseline (EWMA), weighted ~60% HRV / ~25% RHR / ~15% sleep+temperature. Output 0-100 with green/yellow/red bands like WHOOP. This alone matches Athlytic and is achievable in a weekend.
- Define the overnight window from sleepAnalysis and sample HRV/RHR only during sleep to cut daytime noise — this is the single biggest accuracy lever every app relies on. Show 'need 2-3 nights to calibrate' onboarding copy like Bevel, since baselines require history.
- Implement Strain as a Banister-TRIMP integral over the day's heart-rate samples (HR-reserve fraction with exponential weighting) and present it on a WHOOP-style 0-21 log scale. It is well-documented, free of proprietary IP, and pairs naturally with the recovery score for a 'strain vs recovery balance' view.
- Use appleSleepingWristTemperature as a nightly deviation-from-baseline contributor (Ultra 3 supports it), replicating Oura/WHOOP temperature signals. Treat it as a minor weight and an illness/overtraining flag, not a core driver.
- Do NOT depend on SpO2 — automatic Blood Oxygen is disabled on newer US Apple Watches; make it optional/absent so a US Ultra 3 user is not missing a 'required' input.
- Adopt Gentler Streak's gentler framing to fit the calm Claude aesthetic: show recovery as a soft band/guidance ('your body is ready to push' / 'ease off today') rather than an anxiety-inducing hard number, and let the user set a manual status (Sick/Rest) that overrides the algorithm.
- Feed the recovery score directly into the RPG layer: high recovery = bonus XP multiplier or a 'well-rested' avatar buff; chronic low recovery = the avatar looks tired — turning an abstract HRV number into an emotional, game-like signal that is the app's differentiator.
- Keep the math transparent and personal-baseline-relative (never population norms) — every credible app stresses 'compared to YOU.' Display the baseline vs today delta so the single score never feels like a black box.

## Sources
- https://www.whoop.com/us/en/thelocker/how-does-whoop-recovery-work-101/
- https://apps.steradianlabs.com/blog/whoop-strain-score-explained
- https://support.ouraring.com/hc/en-us/articles/360057791533-Readiness-Contributors
- https://athlyticapp.helpscoutdocs.com/article/20-understanding-recovery
- https://trainingtodayapp.helpscoutdocs.com/article/84-how-training-today-works
- https://help.bevel.health/en/articles/11258241
- https://docs.gentler.app/understanding-your-activity-path/what-is-the-activity-path
- https://developer.apple.com/documentation/healthkit/hkquantitytypeidentifier/respiratoryrate
