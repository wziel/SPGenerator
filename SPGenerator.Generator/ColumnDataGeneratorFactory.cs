using SPGenerator.Generator.ColumnDataGenerator;
using SPGenerator.Generator.ColumnDataGenerator.Choice;
using SPGenerator.Generator.ColumnDataGenerator.MultilineText;
using SPGenerator.Generator.ColumnDataGenerator.Number;
using SPGenerator.Generator.ColumnDataGenerator.Text;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator
{
    /// <summary>
    /// Factory responsible for creating column data generators.
    /// </summary>
    public class ColumnDataGeneratorFactory : IColumnDataGeneratorFactory
    {
        private Dictionary<Type, Func<ColumnPOCO, IEnumerable<IColumnDataGenerator>>> getGeneratorsStrategies
            = new Dictionary<Type, Func<ColumnPOCO, IEnumerable<IColumnDataGenerator>>>()
            {
                { typeof(NumberColumnPOCO), GetNumberDataGenerators },
                { typeof(TextColumnPOCO), GetTextDataGenerators },
                { typeof(MultilineTextColumnPOCO), GetMultilineTextDataGenerators },
                { typeof(ChoiceColumnPOCO), GetChoiceDataGenerators }
            };

        public IEnumerable<IColumnDataGenerator> GetDataGenerators(ColumnPOCO columnPOCO)
        {
            var generators = getGeneratorsStrategies[columnPOCO.GetType()].Invoke(columnPOCO);
            return generators.Where(g => g.CanGenerateData);
        }

        private static IEnumerable<IColumnDataGenerator> GetNumberDataGenerators(ColumnPOCO columnPOCO)
        {
            var numberColumnPOCO = (NumberColumnPOCO)columnPOCO;
            return new List<IColumnDataGenerator>()
            {
                new RandomIntegerDataGenerator(numberColumnPOCO),
                new RandomDoubleDataGenerator(numberColumnPOCO),
                new BoundaryDoubleDataGenerator(numberColumnPOCO),
                new BoundaryIntegerDataGenerator(numberColumnPOCO),
                new NullDataGenerator(columnPOCO),
            };
        }

        private static IEnumerable<IColumnDataGenerator> GetTextDataGenerators(ColumnPOCO columnPOCO)
        {
            var textColumnPOCO = (TextColumnPOCO)columnPOCO;
            return new List<IColumnDataGenerator>()
            {
                new DbPlainTextDataGenerator(textColumnPOCO),
                new NullDataGenerator(columnPOCO),
            };
        }

        private static IEnumerable<IColumnDataGenerator> GetMultilineTextDataGenerators(ColumnPOCO columnPOCO)
        {
            var multilineTextColumnPOCO = columnPOCO as MultilineTextColumnPOCO;
            return new List<IColumnDataGenerator>()
            {
                new DbPlainMultilineTextDataGenerator(multilineTextColumnPOCO),
                new NullDataGenerator(columnPOCO),
            };
        }

        private static IEnumerable<IColumnDataGenerator> GetChoiceDataGenerators(ColumnPOCO columnPOCO)
        {
            var choiceColumnPOCO = columnPOCO as ChoiceColumnPOCO;
            return new List<IColumnDataGenerator>()
            {
                new RandomChoiceDataGenerator(choiceColumnPOCO),
                new NullDataGenerator(choiceColumnPOCO),
            };
        }
    }

    public interface IColumnDataGeneratorFactory
    {
        IEnumerable<IColumnDataGenerator> GetDataGenerators(ColumnPOCO columnPOCO);
    }
}
