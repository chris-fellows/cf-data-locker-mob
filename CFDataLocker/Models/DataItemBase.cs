using System.Xml.Serialization;

namespace CFDataLocker.Models
{
    /// <summary>
    /// Base for data items
    /// </summary>
    [XmlInclude(typeof(DataItemDefault))]
    [XmlInclude(typeof(DataItemBankAccount))]
    [XmlInclude(typeof(DataItemCreditCard))]
    [XmlInclude(typeof(DataItemDocument))]
    public abstract class DataItemBase : ICloneable
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Name of data item
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Extra notes
        /// </summary>
        public string Notes { get; set; } = String.Empty;

        public virtual object Clone()
        {
            throw new NotImplementedException();    // Should always be overriden
        }
    }
}
