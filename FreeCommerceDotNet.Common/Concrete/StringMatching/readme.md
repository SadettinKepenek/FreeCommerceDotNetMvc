# Örnek Kullanım

private static bool MatchSourceAndTarget(string target, string source)
        {
            List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();
            options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);
            FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Normal;
            bool result = source.ApproximatelyEquals(target, tolerance, options.ToArray());
            return result;
        }