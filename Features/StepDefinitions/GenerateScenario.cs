using FluentSpec;
using Raconteur;
using Raconteur.Generators;

namespace Features.StepDefinitions
{
    public class GenerateScenario
    {
        private string Runner;

        public void When_the_Scenario_for_a_feature_is_generated()
        {
            var featureFile = new FeatureFile
            {
                Content =  
                @"
                    Feature: Feature Name
                        In order to do something
                        Another thing should happen

                    Scenario: Scenario Name
                "
            };

            Runner = ObjectFactory.NewRunnerGenerator
                .RunnerFor(featureFile);
        }

        public void Then_it_should_be_a_Test_Method()
        {
            Runner.ShouldContain("[TestMethod]");
        }

        public void And_it_should_be_named_After_the_Scenario_name()
        {
            Runner.ShouldContain(@"public void ScenarioName()");            
        }
    }
}