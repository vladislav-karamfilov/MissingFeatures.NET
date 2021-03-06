﻿namespace MissingFeatures.Tests.IEnumerableExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ForEachTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullSourceShouldThrowException()
        {
            IEnumerable<int> source = null;
            Action<int> action = item => { };

            // ReSharper disable once ExpressionIsAlwaysNull
            source.ForEach(action);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullActionShouldThrowException()
        {
            IEnumerable<int> source = new[] { 1, 2, 3, 4, 5 };
            Action<int> action = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            source.ForEach(action);
        }

        [TestMethod]
        public void TestEmptySourceShouldNotInvokeAction()
        {
            IEnumerable<int> source = new int[0];
            var actionWasInvoked = false;
            Action<int> action = item => { actionWasInvoked = true; };

            source.ForEach(action);

            Assert.IsFalse(actionWasInvoked, "Action should not be invoked for an empty enumerable.");
        }

        [TestMethod]
        public void TestOneItemSourceShouldInvokeActionOnce()
        {
            IEnumerable<int> source = new[] { 1 };
            var actionWasInvoked = false;
            var actionInvocationsCount = 0;
            Action<int> action = item =>
            {
                actionWasInvoked = true;
                actionInvocationsCount++;
            };

            source.ForEach(action);

            Assert.IsTrue(actionWasInvoked, "Action should be invoked for a non-empty enumerable.");
            Assert.AreEqual(1, actionInvocationsCount, "Action should be invoked once for an enumerable with one item.");
        }

        [TestMethod]
        public void TestMultipleItemsSourceShouldInvokeActionMultipleTimes()
        {
            IEnumerable<int> source = new[] { 1, 10, 100, 1000 };
            var actionInvocationsCount = 0;
            Action<int> action = item => { actionInvocationsCount++; };

            source.ForEach(action);

            Assert.AreEqual(source.Count(), actionInvocationsCount, "Action should be invoked N times for an enumerable with N items.");
        }
    }
}
