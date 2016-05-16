namespace Thermory.Web.Models.Status
{
    public abstract class StatusLabel
    {
        public abstract string CssClass { get; }
        
        public virtual string Style { get { return "font-weight: normal;"; } }
    }
}
