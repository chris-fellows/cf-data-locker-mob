using System.Xml.Serialization;

namespace CFDataLocker.Models
{
    /// <summary>
    /// Abstract data item. Specific data item classes derive from this.
    /// </summary>
    [XmlInclude(typeof(DataItemDefault))]
    [XmlInclude(typeof(DataItemBankAccount))]
    [XmlInclude(typeof(DataItemCreditCard))]
    [XmlInclude(typeof(DataItemDocument))]
    public abstract class DataItemBase : ICloneable
    {
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string Notes { get; set; } = String.Empty;

        public virtual object Clone()
        {
            throw new NotImplementedException();    // Should always be overriden
        }
    }
}
