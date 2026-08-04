# training-science

## Key findings
- Apple's own Training Load (watchOS 11+) is the native baseline a solo app can lean on: each workout gets an Effort score 1-10 (auto-estimated for cardio from age/height/weight/GPS pace/HR/elevation, or manual for strength), and the app shows a 7-day acute load vs 28-day chronic load trend. It needs 28 days to build a baseline. This is essentially a session-RPE / sRPE load model dressed as effort ratings.
- Heart-rate zone models rank by personalization: age-based %HRmax (220-age) is the crudest; Karvonen / %HRR (Heart Rate Reserve) is materially better because it anchors to measured resting HR; LTHR-anchored zones are best because they anchor to a threshold the user actually measured. HealthKit exposes restingHeartRate directly, so Karvonen is cheap to implement well.
- TRIMP (Banister, 1970s) is the foundational internal-load metric from HR: it weights session duration by an exponential function of HR-reserve fraction so intensity counts disproportionately. Edwards (zone-based, multipliers 1-5) is the most practical variant for a solo app because it only needs time-in-zone; Lucia (3 ventilatory zones) and iTRIMP (individual lactate curve) require lab thresholds most users won't have.
- The Coggan TSS/CTL/ATL/TSB model is the most widely adopted training-load framework: CTL = 42-day exponentially-weighted moving average of daily load (Fitness), ATL = 7-day EWMA (Fatigue), TSB = CTL - ATL (Form). It was built on cycling power but the CTL/ATL/TSB math is metric-agnostic and works on any per-session load score (TSS, TRIMP, or Apple Effort).
- ACWR (acute:chronic workload ratio) is heavily criticized in current sports-science literature and should be treated with caution, not as an injury predictor. Impellizzeri et al. (2020) document mathematical coupling (acute load sits inside the chronic denominator, creating spurious correlation), statistical artifacts, inflated odds ratios, and lack of demonstrated causality. The 'sweet spot 0.8-1.3 / danger >1.5' guidance is not causally established.
- Apple Watch VO2max is a submaximal estimate combining a physiological ODE model with a deep neural network; Apple claims ~4% accuracy but an independent 2024 validation found mean underestimation of ~6 mL/kg/min (MAPE ~13%), so it is reliable for tracking personal trends over time, not for absolute cross-person comparison. It only updates during outdoor walk/run/hike on <5% grade with good GPS+HR and ~30% HR rise above rest.
- HRV-guided training (adjust today's intensity based on morning HRV) has promising but inconclusive evidence: meta-analyses and RCTs show HRV-guided groups often match or slightly beat fixed plans, but no consensus of clear superiority. It's a defensible 'readiness' signal for a personal app, not a proven performance edge. HealthKit exposes heartRateVariabilitySDNN.
- Running power (Stryd) approximates metabolic cost as watts and its Critical Power corresponds roughly to VT2/lactate threshold; it's highly predictive of race performance. But it requires a foot pod or a wrist estimate; the underlying watts derive from the ACSM running VO2 equation (VO2 = 3.5 + 0.2 x speed_m/min) converted to watts at ~25% efficiency. A solo Apple-only app cannot get true running power without a Stryd.

## Specifics
- Banister TRIMP = duration_min x HRr x Y, where HRr = (HR_ex - HR_rest)/(HR_max - HR_rest); Y = 0.64 x e^(1.92 x HRr) for men, Y = 0.86 x e^(1.67 x HRr) for women.
- Edwards TRIMP = sum over 5 zones of (minutes_in_zone x zone_weight), weights Z1=1, Z2=2, Z3=3, Z4=4, Z5=5. Zones typically 50-60/60-70/70-80/80-90/90-100% HRmax. Only needs time-in-zone from the workout HR stream.
- Lucia TRIMP: 3 zones split by ventilatory thresholds VT1 and VT2, weights 1/2/3, summed over minutes in each zone.
- Karvonen / HRR target HR = HR_rest + intensity_fraction x (HR_max - HR_rest). HRR = HR_max - HR_rest.
- %HRmax zones (age-based): HR_max approx 220 - age (or better, Tanaka: 208 - 0.7 x age). Crudest model; use only as fallback.
- LTHR field test: run a 30-min solo time trial, take average HR of the final 20 min = LTHR. Joe Friel LTHR zones: Z1 <0.81 x LTHR, Z2 0.81-0.89, Z3 0.90-0.93, Z4 (threshold) 0.94-0.99, Z5 >=1.00.
- Coggan TSS = (duration_sec x NP x IF) / (FTP x 3600) x 100; 1 hour at FTP = 100 TSS. IF = NP/FTP. NP = 4th-root of the mean of 30-sec rolling-average power to the 4th power.
- hrTSS (heart-rate TSS, for users without power): estimate TSS from time in HR zones relative to LTHR; used by TrainingPeaks when no power file exists.
- CTL (Fitness) = EWMA of daily load, time constant 42 days. Update: CTL_today = CTL_yesterday + (load_today - CTL_yesterday)/42.
- ATL (Fatigue) = EWMA of daily load, time constant 7 days. ATL_today = ATL_yesterday + (load_today - ATL_yesterday)/7.
- TSB (Form) = CTL_yesterday - ATL_yesterday. Positive = fresh/tapered, strongly negative = accumulated fatigue.
- ACWR (coupled) = (7-day rolling avg load) / (28-day rolling avg load). Uncoupled version excludes the acute week from the chronic denominator to reduce mathematical coupling. Flagged 'sweet spot' 0.8-1.3 is not causally validated.
- Apple Training Load: per-workout Effort rating 1-10; cardio auto-scored from age, height, weight, GPS pace, heart rate, elevation; strength/other manual. Displays 7-day (acute) vs 28-day (chronic) comparison; requires 28 days to baseline.
- Apple VO2max algorithm = physiological ODE (HR-to-pace response) + deep neural network + profile (age/sex/height/weight); outputs mL/kg/min. Requires outdoor walk/run/hike, <5% grade, good GPS+HR, ~30% HR increase above resting.
- Independent validation (2024, PMC12080799): Apple Watch VO2max underestimated by mean 6.07 mL/kg/min, MAPE 13.31% vs lab; good for trend tracking not absolute value.
- Running power (ACSM-derived): VO2 (mL/kg/min) = 3.5 + 0.2 x speed_m_per_min (+ 0.9 x speed x grade for incline); convert VO2 to watts via caloric equivalent of O2 and ~25% gross mechanical efficiency.
- Stryd Critical Power (CP) ~ Functional Threshold Power (~60-min max power) ~ VT2/OBLA; highly predictive of race times.
- HealthKit identifiers relevant to load/fitness: HKQuantityTypeIdentifier.vo2Max, .restingHeartRate, .heartRateVariabilitySDNN, .heartRate; workout HR stream via HKLiveWorkoutBuilder / HKWorkout for per-session TRIMP.
- session-RPE (sRPE) load = RPE (Borg CR10, 1-10) x session_duration_min; Foster's method, the simplest possible internal-load metric and effectively what Apple's Effort score computes.
- HRr in Banister uses the same HR-reserve fraction as Karvonen, so a single computed HRR pipeline feeds both zones and TRIMP.

## Recommendations
- Compute a per-workout internal load score for every session using Edwards TRIMP from the HealthKit workout HR stream (needs only time-in-zone) as the primary engine, with session-RPE (RPE x duration) as the manual fallback for strength/non-HR sessions. This mirrors what Apple's Effort score does and works for all workout types.
- Feed that daily load into the Coggan CTL(42d)/ATL(7d)/TSB EWMA math to drive the app's Fitness / Fatigue / Form display. This is the single highest-value, best-validated, metric-agnostic framework and it maps cleanly onto the RPG leveling layer (CTL = character 'level/power', TSB = current 'readiness/energy bar').
- Build zones on Karvonen/%HRR using HealthKit restingHeartRate (auto), not naive %HRmax, and let the user optionally set an LTHR from a 30-min field test to upgrade to threshold-anchored zones. Cache HRR once and reuse it for both zone coloring and TRIMP.
- Read Apple's native VO2max (HKQuantityTypeIdentifier.vo2Max) and Training Load rather than re-deriving them; present VO2max as a trend line with an explicit 'estimate, track the trend not the number' disclaimer, since independent validation shows ~13% error.
- Include a readiness/recovery signal from morning heartRateVariabilitySDNN + restingHeartRate as a soft daily suggestion (train hard / go easy), but frame it as guidance not gospel, matching the inconclusive HRV-guided-training evidence. This is a natural input to the avatar's daily 'energy' stat.
- If you show ACWR at all, show it as a descriptive load-trend line, not an injury-risk score, and avoid 'danger zone' warnings; the research does not support causal injury prediction. Prefer TSB (form) as the fatigue-management metric instead.
- Do not attempt true running power without a Stryd; if you want a power-like number, derive an estimated running-power/pace-effort from the ACSM VO2 equation and GPS pace, clearly labeled as an estimate. Otherwise skip running power for v1 to keep the app minimal.
- Persist one clean pipeline: HealthKit workout -> per-session load (Edwards TRIMP or sRPE) -> daily load total -> CTL/ATL/TSB. Everything else (zones, VO2max trend, HRV readiness) is a read-only display layer on top.

## Sources
- https://www.apple.com/newsroom/2024/06/watchos-11-brings-powerful-health-and-fitness-insights/
- https://journals.humankinetics.com/view/journals/ijspp/15/6/article-p907.xml
- https://www.empirical.health/blog/how-apple-watch-cardio-fitness-vo2max-works/
- https://www.ncbi.nlm.nih.gov/pmc/articles/PMC12080799/
- https://www.mdpi.com/2076-3417/10/23/8532
- https://medium.com/critical-powers/formulas-from-training-and-racing-with-a-power-meter-2a295c661b46
- https://www.ncbi.nlm.nih.gov/pmc/articles/PMC10649254/
- https://developer.apple.com/documentation/healthkit/hkquantitytypeidentifier/heartratevariabilitysdnn
