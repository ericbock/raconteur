using System.Linq;
using FluentSpec;
using Raconteur;
using Raconteur.IDEIntegration.SyntaxHighlighting.Classification;
using Raconteur.IDEIntegration.SyntaxHighlighting.Parsing;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Features.SyntaxHighlight
{
    public class HighlightFeature : FeatureRunner
    {
        protected FeatureTagParser Sut
        {
            get { return new FeatureTagParser(new SUT(Feature), Feature); }
        }

        public override void Given_the_Feature_contains(string Feature) 
        {
            Given_the_Feature_is(Feature.TrimLines());
        }

        protected void Raconteur_should_highlight(int Count, string Text, string Style)
        {
            Sut.Tags.Where(Tag => 
                Tag.Text == Text && 
                FeatureClassifier.Styles[Tag.Type] == Style)
                .Count().ShouldBe(Count, "Did not find " + Count + " " + Text + " " + Style);
        }

        protected void Raconteur_should_not_highlight(string Text)
        {
            Sut.Tags.Any
            (
                Tag => Tag.Text.TrimLines() == Text.TrimLines()
            ).ShouldBeFalse("Highlighted " + Text.Quoted());
        }

        protected void Raconteur_should_highlight_like_a(string Style, int Count, string Text)
        {
            Raconteur_should_highlight(Count, Text, Style);

        }

        protected void Raconteur_should_highlight___like_a(int Count, string Text, string Style)
        {
            Raconteur_should_highlight(Count, Text, Style);
        }

        protected void Raconteur_should_highlight_like_a(string Style, string Text)
        {
            Sut.Tags.Any(Tag => 
                Tag.Text.TrimLines() == Text.TrimLines() && 
                FeatureClassifier.Styles[Tag.Type] == Style)
                .ShouldBeTrue("Did not highlight " + Text.Quoted() + " like a " + Style.Quoted());
        }

        class SUT : FeatureTokenTagger
        {
            public SUT(string Feature) { this.Feature = Feature; }

            public override ITagSpanWrap<FeatureTokenTag> CreateTag(int startLocation, int length, FeatureTokenTypes type)
            {
                var Tag = Create.TestObjectFor<ITagSpanWrap<FeatureTokenTag>>();

                Given.That(Tag).Text.Is(Feature.Substring(startLocation, length));
                Given.That(Tag).Type.Is(type);

                return Tag;
            }
        }
    }
}