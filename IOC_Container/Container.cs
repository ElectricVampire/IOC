using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IOC_Container
{
    /// <summary>
    /// Container which will register and resolve dependencies
    /// </summary>
    public class MyIOCContainer
    {
        /// <summary>
        /// Collection to store dependencies which are registered
        /// </summary>
        private Dictionary<Type, Type> dependencyMap = new Dictionary<Type, Type>();

        /// <summary>
        /// Register the dependencies in container
        /// </summary>
        /// <typeparam name="TypeToResolve">Type of which object is required</typeparam>
        /// <typeparam name="ResolvedType">Type in which object is required.Generally an interface object</typeparam>
        /// <returns>true if dependency is registered otherwise false</returns>
        public bool Register<ResolvedType, TypeToResolve>()
        {
            bool registered = false;
            if (dependencyMap.ContainsKey(typeof(ResolvedType)))
            {
                // Perhaps an exception should be thrown from here?
                Console.WriteLine(typeof(ResolvedType).FullName + " is already registered");
            }
            else
            {
                dependencyMap.Add(typeof(ResolvedType), typeof(TypeToResolve));
                registered = true;
            }

            return registered;
        }

        /// <summary>
        /// Resolves the Type T and returns resolved type from dependencyMap
        /// TO-DO: What happens if someone try to resolve two concrete class dependencies implementing same interface
        /// Currently it will tell dependency is already registered...How do i distinguish two classes from each other while 
        /// resolving their type (Make sure after implementation high level don't have dependency on low level module)
        /// </summary>
        /// <typeparam name="T">Type which need to be resolved</typeparam>
        /// <returns>returns resolved type from dependencyMap</returns>
        
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        private object Resolve(Type resolvedType)
        {
            object resolvedObj = null;
            if (dependencyMap.ContainsKey(resolvedType))
            {
                try
                {
                    Type typeToResolve = dependencyMap[resolvedType];
                    // Do i need to invoke parameterized constructor?
                    // Get first constructor
                    ConstructorInfo ctorInfo = resolvedType.GetConstructors()[0];

                    // Get parameters
                    ParameterInfo[] paramInfo = ctorInfo.GetParameters();
                    if(paramInfo.Length!=0)
                    {
                        List<object> parameters = new List<object>(paramInfo.Length);
                        foreach (ParameterInfo param in paramInfo)
                        {
                            parameters.Add(Resolve(param.ParameterType));
                        }

                        // invoked parameterized constructor
                        resolvedObj = ctorInfo.Invoke(parameters.ToArray());
                    }
                    else
                    {
                        // parameter length is zero invoke parameterless constructor
                        resolvedObj = Activator.CreateInstance(resolvedType);
                    }
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Console.WriteLine(resolvedType.FullName + " is not a registered dependency");
            }

            return resolvedObj;
        }

    }
}
