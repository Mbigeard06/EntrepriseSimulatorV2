﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// A product build by the company
    /// </summary>
    abstract public class Product
    {
        /// <summary>
        /// Gets the amount of materials needed to build the product
        /// </summary>
        abstract public int MaterialsNeeded { get; }
        /// <summary>
        /// Gets the number of employees needed to build the product
        /// </summary>
        abstract public int EmployeesNeeded { get; }
        /// <summary>
        /// Gets the amount of time needed to build the product
        /// </summary>
        abstract public int TimeToBuild { get; }

        /// <summary>
        /// Gets the name of the product
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the space needed to stock the product
        /// </summary>
        public abstract int SpaceToStock { get; }

        /// <summary>
        /// Gets the price of the product
        /// </summary>
        public abstract int Price { get; }

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   MaterialsNeeded == product.MaterialsNeeded &&
                   EmployeesNeeded == product.EmployeesNeeded &&
                   TimeToBuild == product.TimeToBuild &&
                   Name == product.Name &&
                   SpaceToStock == product.SpaceToStock &&
                   Price == product.Price;
        }
    }
}
