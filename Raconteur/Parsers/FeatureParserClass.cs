﻿using System;
using System.Text.RegularExpressions;
using Raconteur.Generators;
using Raconteur.IDE;

namespace Raconteur.Parsers
{
    public class FeatureParserClass : FeatureParser 
    {
        string Content;
        public ScenarioTokenizer ScenarioTokenizer { get; set; }

        public Feature FeatureFrom(FeatureFile FeatureFile, FeatureItem FeatureItem)
        {
            if (FeatureFile == null) return new Feature();
            Content = FeatureFile.Content;

            return new Feature
            {
                FileName = FeatureFile.Name,
                Namespace = FeatureItem.DefaultNamespace,
                Name = Name,
                Scenarios = ScenarioTokenizer.ScenariosFrom(Content)
            };
        }

        string Name 
        { 
            get 
            {
                var Regex = new Regex(@"Feature: (\w.+)(" + 
                    Environment.NewLine + "|$)");
            
                var Match = Regex.Match(Content);

                return Match.Groups[1].Value.CamelCase();
            }
        }
    }
}