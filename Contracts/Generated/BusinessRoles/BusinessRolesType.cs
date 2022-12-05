namespace Retalix.Contracts.Generated.BusinessRoles
{
    using Retalix.Contracts.Generated.Common;
    using Retalix.Contracts.Generated.Arts.PosLogV6.Source;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("BatchContractGenerator.Console", "30.100.999")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://retalix.com/R10/services")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://retalix.com/R10/services", IsNullable=true)]
    [Retalix.Commons.Contracts.ContractDocumentationAttributes.ContractSourceAttribute("Schemas\\BusinessRoles\\BusinessRoles.xsd")]
    public partial class BusinessRolesType
    {
        
        private int role_idField;
        
        private string bus_unit_idField;
        
        private string vc_role_codeField;
        
        private string vc_nameField;
        
        private ActionTypeCodes actionField;
        
        private bool actionFieldSpecified;
        
        private bool role_idFieldSpecified;
        
        [Retalix.Commons.Contracts.ContractValidationAttributes.ContractRequiredAttribute()]
        public int role_id
        {
            get
            {
                return this.role_idField;
            }
            set
            {
                this.role_idField = value;
                this.role_idSpecified = true;
            }
        }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        public string bus_unit_id
        {
            get
            {
                return this.bus_unit_idField;
            }
            set
            {
                this.bus_unit_idField = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        public string vc_role_code
        {
            get
            {
                return this.vc_role_codeField;
            }
            set
            {
                this.vc_role_codeField = value;
            }
        }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        public string vc_name
        {
            get
            {
                return this.vc_nameField;
            }
            set
            {
                this.vc_nameField = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ActionTypeCodes Action
        {
            get
            {
                return this.actionField;
            }
            set
            {
                this.actionField = value;
                this.ActionSpecified = true;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActionSpecified
        {
            get
            {
                return this.actionFieldSpecified;
            }
            set
            {
                this.actionFieldSpecified = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool role_idSpecified
        {
            get
            {
                return this.role_idFieldSpecified;
            }
            set
            {
                this.role_idFieldSpecified = value;
            }
        }
    }
}
