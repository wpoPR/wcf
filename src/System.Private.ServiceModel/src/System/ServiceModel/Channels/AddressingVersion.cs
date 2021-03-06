// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Xml;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
    public sealed class AddressingVersion
    {
        private string _ns;
        private XmlDictionaryString _dictionaryNs;
        private MessagePartSpecification _signedMessageParts;
        private string _toStringFormat;
        private string _anonymous;
        private XmlDictionaryString _dictionaryAnonymous;
        private Uri _anonymousUri;
        private Uri _noneUri;
        private string _faultAction;
        private string _defaultFaultAction;

        private static AddressingVersion s_none = new AddressingVersion(AddressingNoneStrings.Namespace, XD.AddressingNoneDictionary.Namespace,
            SR.AddressingNoneToStringFormat, new MessagePartSpecification(), null, null, null, null, null);

        private static AddressingVersion s_addressing10 = new AddressingVersion(Addressing10Strings.Namespace,
            XD.Addressing10Dictionary.Namespace, SR.Addressing10ToStringFormat, Addressing10SignedMessageParts,
            Addressing10Strings.Anonymous, XD.Addressing10Dictionary.Anonymous, Addressing10Strings.NoneAddress,
            Addressing10Strings.FaultAction, Addressing10Strings.DefaultFaultAction);
        private static MessagePartSpecification s_addressing10SignedMessageParts;


        private AddressingVersion(string ns, XmlDictionaryString dictionaryNs, string toStringFormat,
            MessagePartSpecification signedMessageParts, string anonymous, XmlDictionaryString dictionaryAnonymous, string none, string faultAction, string defaultFaultAction)
        {
            _ns = ns;
            _dictionaryNs = dictionaryNs;
            _toStringFormat = toStringFormat;
            _signedMessageParts = signedMessageParts;
            _anonymous = anonymous;
            _dictionaryAnonymous = dictionaryAnonymous;

            if (anonymous != null)
            {
                _anonymousUri = new Uri(anonymous);
            }

            if (none != null)
            {
                _noneUri = new Uri(none);
            }

            _faultAction = faultAction;
            _defaultFaultAction = defaultFaultAction;
        }


        public static AddressingVersion WSAddressing10
        {
            get { return s_addressing10; }
        }

        public static AddressingVersion None
        {
            get { return s_none; }
        }

        internal string Namespace
        {
            get { return _ns; }
        }

        private static MessagePartSpecification Addressing10SignedMessageParts
        {
            get
            {
                if (s_addressing10SignedMessageParts == null)
                {
                    MessagePartSpecification s = new MessagePartSpecification(
                        new XmlQualifiedName(AddressingStrings.To, Addressing10Strings.Namespace),
                        new XmlQualifiedName(AddressingStrings.From, Addressing10Strings.Namespace),
                        new XmlQualifiedName(AddressingStrings.FaultTo, Addressing10Strings.Namespace),
                        new XmlQualifiedName(AddressingStrings.ReplyTo, Addressing10Strings.Namespace),
                        new XmlQualifiedName(AddressingStrings.MessageId, Addressing10Strings.Namespace),
                        new XmlQualifiedName(AddressingStrings.RelatesTo, Addressing10Strings.Namespace),
                        new XmlQualifiedName(AddressingStrings.Action, Addressing10Strings.Namespace)
                        );
                    s.MakeReadOnly();
                    s_addressing10SignedMessageParts = s;
                }

                return s_addressing10SignedMessageParts;
            }
        }


        internal XmlDictionaryString DictionaryNamespace
        {
            get { return _dictionaryNs; }
        }

        internal string Anonymous
        {
            get { return _anonymous; }
        }

        internal XmlDictionaryString DictionaryAnonymous
        {
            get { return _dictionaryAnonymous; }
        }

        internal Uri AnonymousUri
        {
            get { return _anonymousUri; }
        }

        internal Uri NoneUri
        {
            get { return _noneUri; }
        }

        internal string FaultAction   // the action for addressing faults
        {
            get { return _faultAction; }
        }

        internal string DefaultFaultAction  // a default string that can be used for non-addressing faults
        {
            get { return _defaultFaultAction; }
        }

        internal MessagePartSpecification SignedMessageParts
        {
            get
            {
                return _signedMessageParts;
            }
        }

        public override string ToString()
        {
            return SR.Format(_toStringFormat, Namespace);
        }
    }
}
