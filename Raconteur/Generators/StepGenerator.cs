﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace Raconteur.Generators
{
    public class StepGenerator : CodeGenerator
    {
        private const string StepExecution = 
@"        
            {0}({1});";

        private const string MultilineStepExecution = 
@"        
            {0}
            ({1}
            );";

        private const string StepRowExecution = 
@"        
                new[] {{{0}}},";

        readonly Step Step;
        readonly List<Type> StepLibraries;

        public StepGenerator(Step Step, List<Type> StepLibraries = null)
        {
            this.Step = Step;
            this.StepLibraries = StepLibraries;
        }

        public string Code
        {
            get
            {
                return Step.HasTable ?
                    CodeForStepWithTable :
                    CodeFor(Step);
            }
        }

        string CodeForStepWithTable
        {
            get
            {
                return Step.Table.HasHeader ? 
                    CodeForStepTableWithHeader :
                    CodeForStepTable;
            }
        }

        string CodeForStepTable
        {
            get
            {
                var Table = Step.Table.Rows
                    .Aggregate("", (Steps, Row) => Steps + 
                        string.Format(StepRowExecution, ArgsFrom(Row)));

                return string.Format
                (
                    MultilineStepExecution, 
                    Step.Name, 
                    Table.RemoveTail(1)
                );
            }
        }

        string CodeForStepTableWithHeader
        {
            get
            {
                return Step.Table.Rows.Skip(1)
                    .Aggregate(string.Empty, (Steps, Row) => 
                        Steps + ExecuteStepRow(Row));
            }
        }

        string ExecuteStepRow(IEnumerable<string> Row) 
        { 
            return CodeFor
            (
                new Step
                {
                    Name = Step.Name,
                    Args = Step.Args.Concat(Row).ToList()
                }
            );
        }

        string ArgsFrom(IEnumerable<string> Row) 
        { 
            var ArgsValues = Step.Args
                .Concat(Row)
                .Select(ArgFormatter.ValueOf);

            return string.Join(", ", ArgsValues);
        }

        string CodeFor(Step Step) 
        {
            var ArgsValues = Step.Args.Select(ArgFormatter.ValueOf);

            var Args = string.Join(", ", ArgsValues);

            return string.Format(StepExecution, NameOf(Step), Args);
        }

        string NameOf(Step Step)
        {
            if (StepLibraries.IsEmpty()) return Step.Name;

            var LibraryWithStep = StepLibraries
                .Where(l => l.GetMethod(Step.Name) != null)
                .FirstOrDefault();

            return LibraryWithStep == null ? Step.Name :
                LibraryWithStep.Name + "." + Step.Name;
        }
    }
}