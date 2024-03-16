// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace EntitledLearningTools.Models;

public abstract class TestModelBase
{
    private static readonly string[] FirstNames = { "Alice", "Bob", "Charlie", "David", "Eva", "Frank", "Grace", "Henry", "Ivy", "Jack" };
    private static readonly string[] LastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Miller", "Davis", "García", "Rodriguez", "Martínez" };
    protected static string GenerateRandomFirstName()
    {
        Random random = new Random();
        int index = random.Next(FirstNames.Length);
        return FirstNames[index];
    }

    protected static string GenerateRandomLastName()
    {
        Random random = new Random();
        int index = random.Next(LastNames.Length);
        return LastNames[index];
    }
}



