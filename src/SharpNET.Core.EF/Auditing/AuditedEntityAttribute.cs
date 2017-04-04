using System;
using System.Collections.Generic;
using System.Text;

namespace SharpNET.Core.EF.Auditing
{
    public class AuditedEntityAttribute : Attribute
    {
        public string AuditEntityTypeName { get; set; }

        public AuditedEntityAttribute(string auditEntityTypeName)
        {
            AuditEntityTypeName = auditEntityTypeName;
        }
    }
}
