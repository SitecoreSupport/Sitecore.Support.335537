using Sitecore.DataExchange;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace Sitecore.Support.DataExchange.Converters.DataAccess.Readers
{
    [SupportedIds("{970FAE22-66D2-4F79-85ED-8C7A3FC6CF4D}")]
    public class CodeToDescriptionValueReaderConverter : BaseItemModelConverter<IValueReader>
    {
        public const string FieldNameCodeDefinitionSet = "CodeDefinitionSet";
        public const string FieldNameCodeDefinitionValue = "Value";
        public CodeToDescriptionValueReaderConverter(IItemModelRepository repository) : base(repository)
        {
        }
        protected override ConvertResult<IValueReader> ConvertSupportedItem(ItemModel source)
        {
            var codeSetItem = this.GetReferenceAsModel(source, FieldNameCodeDefinitionSet);
            if (codeSetItem == null)
            {
                return NegativeResult(source, "The field does not reference a valid item.", $"field: {FieldNameCodeDefinitionSet}");
            }
            var codeItems = this.GetChildItemModels(codeSetItem);
            var reader = new Sitecore.Support.DataExchange.DataAccess.Readers.CodeToDescriptionValueReader();
            foreach (var codeItem in codeItems)
            {
                var value = this.GetIntValue(codeItem, FieldNameCodeDefinitionValue);
                reader.Values[value] = codeItem[ItemModel.ItemName].ToString();
            }
            return PositiveResult(reader);
        }
    }
}