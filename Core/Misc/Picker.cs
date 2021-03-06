using System;
using System.Collections;
using System.Collections.Generic;

namespace Teuria;

public class Picker<T>
{
    private List<Option> options;

    public float EvaluatedWeight { get; private set; }
    public bool CanPick => EvaluatedWeight > 0;
    public int Count => options.Count;

    public Picker() 
    {
        options = new List<Option>();
    }

    public Picker(T firstOption, float weight) : this()
    {
        AddOption(firstOption, weight);
    }

    public void AddOption(T option, float weight) 
    {
        var w = weight;
        w = Math.Max(weight, 0);
        EvaluatedWeight += weight;
        options.Add(new Option(option, w));
    }

    public T Pick() 
    {   
        if (options.Count == 1)
            return options[0].Value;
        if (CanPick) 
        {
            var w = 0f;
            var roll = MathUtils.Randomizer.NextDouble() * EvaluatedWeight;
            var optionCount = options.Count - 1;
            
            for (int i = 0; i < optionCount; i++) 
            {
                var option = options[i];
                w += option.Weight;
                if (roll < w)
                    return option.Value;
            }
            return options[optionCount].Value;
        }
        return default;
    }

    private class Option 
    {
        public T Value;
        public float Weight;

        public Option(T value, float weight) 
        {
            Value = value;
            Weight = weight;
        }
    }
}