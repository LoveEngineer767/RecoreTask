namespace InterviewExercises.PalindromeChecker;

public static class PalindromeChecker
{
    public static bool IsPalindrome(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var left = 0;
        var right = value.Length - 1;

        while (left < right)
        {
            if (!char.IsLetterOrDigit(value[left]))
            {
                left++;
                continue;
            }

            if (!char.IsLetterOrDigit(value[right]))
            {
                right--;
                continue;
            }

            if (char.ToLowerInvariant(value[left]) != char.ToLowerInvariant(value[right]))
            {
                return false;
            }

            left++;
            right--;
        }

        return true;
    }
}
