namespace Thermory.Web.Models.Status
{
    public class DangerLabel : StatusLabel
    {
        public override string CssClass { get { return "label label-danger"; } }
    }
}