using System;

namespace Thermory.Web.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerImplementationNotesAttribute : Attribute
    {
        public string ImplementationNotes { get; private set; }

        public SwaggerImplementationNotesAttribute(string implementationNotes)
        {
            ImplementationNotes = implementationNotes;
        }
    }
}