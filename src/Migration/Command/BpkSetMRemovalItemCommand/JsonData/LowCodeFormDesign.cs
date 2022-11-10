using System.Text.Json.Serialization;

namespace Migration.CLI.Command.BpkSetMRemovalItemCommand.JsonData
{
    internal class LowCodeFormDesign
    {
        [JsonPropertyName("fields")]
        public List<LowCodeFormDesignField>? Fields { get; set; }
    }

    internal class LowCodeFormDesignField
    {
        [JsonPropertyName("__config__")]
        public LowCodeFormDesignFieldConfig? Config { get; set; }

        [JsonPropertyName("__vModel__")]
        public string? VModel { get; set; }

        [JsonPropertyName("formId")]
        public long? FormId { get; set; }
    }

    internal class LowCodeFormDesignFieldConfig
    {
        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("dataTypeCode")]
        public string? DataTypeCode { get; set; }
    }
}
