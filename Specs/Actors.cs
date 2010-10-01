﻿using System.Collections.Generic;
using Raconteur;

namespace Specs
{
    public class Actors
    {
        public const string FeatureWithThreeScenarios = 
        @"
            Feature: has three scenarios
            Scenario: First Scenario
            Scenario: Second Scenario
            Scenario: Third Scenario
        ";

        public const string StepDefinitionsForFeatureWithOneScenario = 
        @"
namespace  
{
    public partial class HasOneScenario 
    {
    }
}
";

        public const string FeatureWithOneScenario = 
        @"
            Feature: has one scenario
            Scenario: One Scenario
        ";

        public const string FeatureWithNoScenarios = @"Feature: Feature Name";

        public const string ScenarioWithNoSteps = @"Scenario: Scenario Name";

        public const string ScenarioWithOneStep = 
        @"
            Scenario: has one step
            This is a step
        ";

        public const string ScenarioWithThreeSteps = 
        @"
            Scenario: has three steps
            This is one step
            This is another step
            This is the last step
        ";

        public const string DefinitionCode =
        @"
            namespace Feature.StepDefinitions
            {
                public class Definition
                {

                }
            }
        ";

        public readonly static Feature Feature = new Feature
        {
            Name = "Name",
            FileName = "File Name",
            Namespace = "Features",
            Scenarios =
                {
                    new Scenario
                    {
                        Name = "Scenario 1",
                        Steps = 
                        { 
                            new Step{ Name = "Unique step" }, 
                            new Step{ Name = "Repeated step" }
                        }
                    },                            
                    new Scenario
                    {
                        Name = "Scenario 2",
                        Steps =
                        {
                            new Step{ Name = "Repeated step" }, 
                            new Step{ Name = "Another unique step" }
                        }
                    },                            
                }
        };

        public static Feature FeatureWithArgs = new Feature
        {
            Name = "Name",
            FileName = "File Name",
            Namespace = "Features",
            Scenarios =
            {
                new Scenario
                {
                    Name = "Scenario_1",
                    Steps = 
                    { 
                        new Step
                        { 
                            Name = "If__happens",
                            Args = new List<string>{"\"X\""},
                        },
                        new Step
                        { 
                            Name = "If__and__happens",
                            Args = new List<string>{"\"X\"", "\"Y\""}
                        },
                    }
                }
            }                            
        };

        public static class DefinedFeature
        {
            public const string FeatureDefinition =
            @"
                Feature: Feature Name
                Scenario: has one step
                This is a step
            ";

            public const string StepsDefinition =
            @"
                public partial class FeatureName
                {

                    public void This_is_one_step() {

                        var thing = FeatureName;
                    }
                }
            ";

            public const string StepsDefinitionWithBase =
            @"
                public partial class FeatureName : BaseClass
                {

                    public void This_is_one_step() {

                        var thing = FeatureName;
                    }
                }
            ";

            public const string RenamedFeatureDefinition =
            @"
                Feature: Renamed Feature
                Scenario: has one step
                This is a step
            ";

            public const string RenamedStepsDefinition =
            @"
                public partial class RenamedFeature
                {

                    public void This_is_one_step() {

                        var thing = FeatureName;
                    }
                }
            ";
        }
    }
}