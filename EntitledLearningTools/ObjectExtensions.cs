using System;
using System.Collections.Generic;
using System.Reflection;

namespace EntitledLearningTools;

public static class ObjectExtensions
{
    public static Dictionary<string, object?> ToDictionary(this object obj)
    {
        var dictionary = new Dictionary<string, object?>();

        // Get the type of the object
        Type type = obj.GetType();

        // Get all properties of the type
        PropertyInfo[] properties = type.GetProperties();

        // Iterate through the properties and add them to the dictionary
        foreach (PropertyInfo property in properties)
        {
            // Check if the property is readable
            if (property.CanRead)
            {
                string key = property.Name;
                
                try
                {
                    object? value = property.GetValue(obj);
                    dictionary.Add(key, value);
                }
                catch (Exception ex)
                {
                    // Handle the exception or log it
                    Console.WriteLine($"Error getting value for property {key}: {ex.Message}");
                }
            }
        }

        return dictionary;
    }
}