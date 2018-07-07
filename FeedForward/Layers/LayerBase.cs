﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FeedForward.Core;

namespace FeedForward.Layers
{
    abstract class LayerBase
    {

        public int nodes;

        public Matrix values;
        public Matrix weights;
        public Matrix errors;
        public Matrix bias;

        public abstract void FeedForward(LayerBase input);
        public abstract void Backpropagate(LayerBase output);

        public abstract void initWeights();

    }
}