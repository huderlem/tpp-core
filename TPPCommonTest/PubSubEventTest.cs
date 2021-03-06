﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TPPCommon.PubSub;
using TPPCommon.PubSub.Events;
using Xunit;

namespace TPPCommonTest
{
    /// <summary>
    /// Test definition of PubSub event classes and their respective topic.
    /// </summary>
    public class PubSubEventTest
    {
        private const string TestTopic = "test_topic"; // can be any topic 
        
        [Topic(TestTopic)]
        class TestEventWithTopicAttribute : PubSubEvent
        {
        }

        class TestEventWithoutTopicAttribute : PubSubEvent
        {
        }
        
        [Topic(TestTopic)]
        class TestEventNotSubclass
        {
        }

        [Fact]
        public void TestTopicNameWithAttribute()
        {
            var @event = new TestEventWithTopicAttribute();
            string expected = TestTopic + TopicAttribute.Suffix;
            Assert.Equal(expected, @event.GetTopic());
        }
        
        [Fact]
        public void TestTopicNameWithoutAttribute()
        {
            // retrieving the topic for an event without the topic-attribute should raise an error.
            var @event = new TestEventWithoutTopicAttribute();
            Assert.Throws<ArgumentException>(() => @event.GetTopic());
        }

        [Fact]
        public void TestTopicForNonSubclass()
        {
            // retrieving the topic for a class that doesn't inherit the pubsub event class should raise an error.
            Assert.Throws<ArgumentException>(() => PubSubEvent.GetTopicForEventType(typeof(TestEventNotSubclass)));
        }

        [Fact]
        public void TestTopicInvalidContainsReservedSuffixCharacter()
        {
            string invalidTopic = "contains_reserved_suffix" + TopicAttribute.Suffix;
            Assert.Throws<ArgumentException>(() => new TopicAttribute(invalidTopic));
        }

        [Fact]
        public void TestTopicUniqueness()
        {
            // Gather list of all PubSubEvent sub-classes.
            IEnumerable<Type> pubSubEventTypes = typeof(PubSubEvent).GetTypeInfo().Assembly.GetTypes()
                .Where(type => typeof(PubSubEvent).IsAssignableFrom(type) && type != typeof(PubSubEvent));

            // Ensure all PubSubEvents have unique topics.
            HashSet<string> topics = new HashSet<string>();
            foreach (Type eventType in pubSubEventTypes)
            {
                string topic = PubSubEvent.GetTopicForEventType(eventType);
                Assert.True(topics.Add(topic));
            }
        }
    }
}