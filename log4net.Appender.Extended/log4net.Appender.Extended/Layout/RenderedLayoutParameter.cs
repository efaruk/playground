namespace log4net.Appender.Extended.Layout
{
    public class RenderedLayoutParameter
    {
        public RenderedLayoutParameter() { }

        public RenderedLayoutParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}