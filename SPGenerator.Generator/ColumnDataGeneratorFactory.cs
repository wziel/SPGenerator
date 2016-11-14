using SPGenerator.Generator.ColumnDataGenerator;
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
                { typeof(TextColumnPOCO), GetTextDataGenerators }
            };

        public IEnumerable<IColumnDataGenerator> GetDataGenerators(ColumnPOCO columnPOCO)
        {
            var specificGenerators = getGeneratorsStrategies[columnPOCO.GetType()].Invoke(columnPOCO);
            var commonGenerators = getCommonGenerators(columnPOCO);
            return specificGenerators.Concat(commonGenerators);
        }

        private static IEnumerable<IColumnDataGenerator> getCommonGenerators(ColumnPOCO columnPOCO)
        {
            var commonGenerators = new List<IColumnDataGenerator>();
            if(!columnPOCO.Required)
            {
                commonGenerators.Add(new NullDataGenerator(columnPOCO));
            }
            return commonGenerators;
        }

        private static IEnumerable<IColumnDataGenerator> GetNumberDataGenerators(ColumnPOCO columnPOCO)
        {
            var numberColumnPOCO = (NumberColumnPOCO)columnPOCO;
            var generators = new List<IColumnDataGenerator>();
            generators.Add(new IntegerDataGenerator(numberColumnPOCO));
            if(!numberColumnPOCO.OnlyIntegers)
            {
                generators.Add(new DoubleDataGenerator(numberColumnPOCO));
            }
            return generators;
        }

        private static IEnumerable<IColumnDataGenerator> GetTextDataGenerators(ColumnPOCO columnPOCO)
        {
            var textColumnPOCO = (TextColumnPOCO)columnPOCO;
            return new List<IColumnDataGenerator>()
            {
                new ConstantTextDataGenerator(textColumnPOCO),
            };
        }
    }

    public interface IColumnDataGeneratorFactory
    {
        IEnumerable<IColumnDataGenerator> GetDataGenerators(ColumnPOCO columnPOCO);
    }
}
