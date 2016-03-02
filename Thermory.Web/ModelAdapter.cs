namespace Thermory.Web
{
    public class ModelAdapter<T>
    {
        private const string _blankString = "null";
        private const string _blankSpace = " ";

        protected string ScrubBlanks(string value)
        {
            return value == _blankString ? _blankSpace : value;
        }

        public virtual void Save(T model)
        { }

        public void Delete(string id)
        { }
    }
}