namespace InterviewExercises.PermissionsEvaluator;

public sealed record Permission(string User, string Resource, string Action);

public sealed class PermissionEvaluator
{
    private readonly IReadOnlyCollection<Permission> _permissions;

    public PermissionEvaluator(IEnumerable<Permission> permissions)
    {
        _permissions = permissions.ToArray();
    }

    public bool IsAllowed(string user, string resource, string action)
    {
        return _permissions.Any(permission =>
            permission.User == user &&
            permission.Resource == resource &&
            permission.Action == action);
    }
}
