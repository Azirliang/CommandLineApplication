using System.Text.Json.Serialization;

namespace Migration.CLI.Command.BpkSetMRemovalItemCommand.JsonData
{
    internal class LowCode2Code
    {
        [JsonPropertyName("MRemovalSubTableFieldCode")]
        public string? MRemovalSubTableFieldCode { get; set; }

        [JsonPropertyName("SubTableNameFieldCode")]
        public string? SubTableNameFieldCode { get; set; }

        [JsonPropertyName("SubTableSignCodeFieldCode")]
        public string? SubTableSignCodeFieldCode { get; set; }

        [JsonPropertyName("MRemovalItems")]
        public List<LowCode2CodeMRemovalItem>? MRemovalItems { get; set; }
    }

    internal class LowCode2CodeMRemovalItem
    {
        [JsonPropertyName("DynamicDataId")]
        public string? DynamicDataId { get; set; }

        [JsonPropertyName("DisplayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("SignFieldCode")]
        public string? SignFieldCode { get; set; }
    }
}
