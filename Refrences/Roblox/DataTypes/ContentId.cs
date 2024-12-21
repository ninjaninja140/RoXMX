namespace RobloxFiles.DataTypes
{
    public readonly struct ContentId
    {
        public string Value { get; }

        public ContentId(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;

        // Implicit conversion from string to ContentId
        public static implicit operator ContentId(string value) => new ContentId(value);

        // Implicit conversion from ContentId to string
        public static implicit operator string(ContentId contentId) => contentId.Value;
    }

}
