namespace InterviewExercises.PermissionsEvaluator;

public class PermissionEvaluatorTests
{
    [Fact]
    public void IsAllowed_ReturnsTrueForAnExactPermissionMatch()
    {
        var evaluator = new PermissionEvaluator(
        [
            new Permission("alice", "reports", "read")
        ]);

        Assert.True(evaluator.IsAllowed("alice", "reports", "read"));
    }

    [Theory]
    [InlineData("bob", "reports", "read")]
    [InlineData("alice", "reports", "write")]
    [InlineData("alice", "users", "read")]
    public void IsAllowed_ReturnsFalseWhenNoExactPermissionMatches(
        string user, string resource, string action)
    {
        var evaluator = new PermissionEvaluator(
        [
            new Permission("alice", "reports", "read")
        ]);

        Assert.False(evaluator.IsAllowed(user, resource, action));
    }

    [Fact]
    public void IsAllowed_ReturnsFalseWhenThereAreNoPermissions()
    {
        var evaluator = new PermissionEvaluator([]);

        Assert.False(evaluator.IsAllowed("alice", "reports", "read"));
    }

    [Fact]
    public void IsAllowed_ReturnsTrueOnlyForTheMatchingPermissionAmongSeveral()
    {
        var evaluator = new PermissionEvaluator(
        [
            new Permission("alice", "reports", "read"),
            new Permission("alice", "reports", "write"),
            new Permission("alice", "users", "read"),
            new Permission("bob", "reports", "read")
        ]);

        Assert.True(evaluator.IsAllowed("alice", "reports", "write"));
        Assert.False(evaluator.IsAllowed("alice", "users", "write"));
    }

    [Fact]
    public void IsAllowed_IsCaseSensitive()
    {
        var evaluator = new PermissionEvaluator(
        [
            new Permission("Alice", "Reports", "Read")
        ]);

        Assert.False(evaluator.IsAllowed("alice", "Reports", "Read"));
        Assert.False(evaluator.IsAllowed("Alice", "reports", "Read"));
        Assert.False(evaluator.IsAllowed("Alice", "Reports", "read"));
    }
}
