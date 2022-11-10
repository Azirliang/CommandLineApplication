using System.Text.Json.Serialization;

namespace Migration.CLI.Command.BpkSetMRemovalItemCommand.JsonData
{
    internal class LowCode1Code
    {
        [JsonPropertyName("mRemovalTableName")]
        public string? MRemovalTableName { get; set; }

        [JsonPropertyName("mRemovalFieldDisplayName")]
        public string? MRemovalFieldDisplayName { get; set; }

        [JsonPropertyName("mRemoveUserFieldName")]
        public string? MRemoveUserFieldName { get; set; }

        [JsonPropertyName("mRemovalItemList")]
        public List<LowCode1CodeMRemovalItem>? MRemovalItemList { get; set; }
    }

    internal class LowCode1CodeMRemovalItem
    {
        [JsonPropertyName("formId")]
        public string? FormId { get; set; }

        [JsonPropertyName("fieldDisplayName")]
        public string? FieldDisplayName { get; set; }

        [JsonPropertyName("removeUserFieldName")]
        public string? RemoveUserFieldName { get; set; }
    }
}
