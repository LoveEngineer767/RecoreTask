namespace InterviewExercises.PalindromeChecker;

public class PalindromeCheckerTests
{
    [Theory]
    [InlineData("racecar")]
    [InlineData("Racecar")]
    [InlineData("A man, a plan, a canal: Panama!")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("a")]
    public void IsPalindrome_ReturnsTrueForPalindromes(string value)
    {
        Assert.True(PalindromeChecker.IsPalindrome(value));
    }

    [Theory]
    [InlineData("hello")]
    [InlineData("racecars")]
    [InlineData("A man, a plan, a canal: California!")]
    [InlineData("ab")]
    public void IsPalindrome_ReturnsFalseForNonPalindromes(string value)
    {
        Assert.False(PalindromeChecker.IsPalindrome(value));
    }

    [Fact]
    public void IsPalindrome_ThrowsForNull()
    {
        Assert.Throws<ArgumentNullException>(() => PalindromeChecker.IsPalindrome(null!));
    }

    [Theory]
    [InlineData("12321")]
    [InlineData("12 21")]
    [InlineData("A1b2b1a")]
    [InlineData("abba")]
    [InlineData("aba")]
    [InlineData("!!!")]
    public void IsPalindrome_ReturnsTrueForDigitsLengthVariantsAndSymbols(string value)
    {
        Assert.True(PalindromeChecker.IsPalindrome(value));
    }

    [Theory]
    [InlineData("1231")]
    [InlineData("123 12")]
    [InlineData("abca")]
    public void IsPalindrome_ReturnsFalseForNonPalindromicDigitsAndEvenLength(string value)
    {
        Assert.False(PalindromeChecker.IsPalindrome(value));
    }
}
