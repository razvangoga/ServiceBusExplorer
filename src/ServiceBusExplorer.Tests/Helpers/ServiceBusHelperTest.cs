﻿#region Using Directives

using ServiceBusExplorer;
using System;
using NUnit.Framework;

#endregion

namespace ServiceBusExplorer.Tests.Helpers
{
    [TestFixture]
    public class ServiceBusHelperTest
    {
        [Test]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespace_ReturnsRelativePath()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("sb://aaa.test.com/some/path/segments/name"), Is.EqualTo("some/path/segments/name"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespaceTrailingSlash_ReturnsRelativePathWithTrailingSlash()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("sb://aaa.test.com/some/path/segments/name/"), Is.EqualTo("some/path/segments/name/"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespace_Different_Scheme_ReturnsRelativePath()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("http://aaa.test.com/some/path/segments/name"), Is.EqualTo("some/path/segments/name"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespace_Different_Scheme_Port_Specified_ReturnsRelativePath()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com:80/");

            Assert.That(helper.GetAddressRelativeToNamespace("http://aaa.test.com:80/some/path/segments/name"), Is.EqualTo("some/path/segments/name"));
        }

        [TestCase(":80", ":81")]
        [TestCase(":80", "")]
        [TestCase("", ":80")]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespace_Different_Scheme_Different_Port_ReturnsLastSegment(string namespacePort, string port)
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri($"sb://aaa.test.com{namespacePort}/");

            Assert.That(helper.GetAddressRelativeToNamespace($"http://aaa.test.com{port}/some/path/segments/name"), Is.EqualTo("some/path/segments/name"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_AbsoluteUriNotUnderNamespace_ReturnsLastSegment()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("sb://bbb.test.com/some/path/segments/name"), Is.EqualTo("name"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_AbsoluteUriNotUnderNamespaceTrailingSlash_ReturnsLastSegmentWithTrailingSlash()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("sb://bbb.test.com/some/path/segments/name/"), Is.EqualTo("name/"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_RelativeUriWithMultipleSegments_ReturnsLastSegment()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("some/path/segments/name"), Is.EqualTo("name"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_RelativeUriWithMultipleSegmentsTrailingSlash_ReturnsLastSegmentWithTrailingSlash()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("some/path/segments/name/"), Is.EqualTo("name/"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_RelativeUriWithSingleSegment_ReturnsSegment()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("name"), Is.EqualTo("name"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_RelativeUriWithSingleSegmentAndTrailingSlash_ReturnsSegment()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("name/"), Is.EqualTo("name/"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_Slash_ReturnsSlash()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("/"), Is.EqualTo("/"));
        }

        [Test]
        public void GetAddressRelativeToNamespace_Empty_ReturnsEmpty()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace(""), Is.EqualTo(""));
        }

        [Test]
        public void GetAddressRelativeToNamespace_Whitespace_ReturnsWhitespace()
        {
            var helper = new ServiceBusHelper((m, a) => { });
            helper.NamespaceUri = new Uri("sb://aaa.test.com/");

            Assert.That(helper.GetAddressRelativeToNamespace("    "), Is.EqualTo("    "));
        }
    }
}
