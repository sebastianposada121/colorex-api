using ColorexApp.Domain.Enums;

namespace ColorexApp.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public Guid BranchId { get; private set; }

    private User() { }

    private User(Guid id, string email, string passwordHash, string firstName, string lastName, string role, Guid branchId)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        BranchId = branchId;
    }

    public static User Create(string email, string passwordHash, string firstName, string lastName, UserRole role, Guid branchId)
        => new(Guid.NewGuid(), email, passwordHash, firstName, lastName, role.ToString(), branchId);

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateRole(UserRole newRole) => Role = newRole.ToString();
    public void AssignBranch(Guid branchId) => BranchId = branchId;
}
