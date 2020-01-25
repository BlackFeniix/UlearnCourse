using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Profiling
{
    class Generator
    {
        private static string GenerateSingleDeclaration(bool isStruct, double amountOfVariable)
        {
            var declaration = "";
            if (isStruct)
                declaration += "\nstruct S" + amountOfVariable + "\n{\n";
            else
                declaration += "\nclass C" + amountOfVariable + "\n{\n";
            for (var j = 0; j < amountOfVariable; j++)
            {
                declaration += "byte Value" + j + ";";
            }

            declaration += "\n}\n";
            return declaration;
        }

        public static string GenerateDeclarations()
        {
            var command = "";
            for (var i = 0; i < 10; i++)
            {
                var amountOfVariable = Math.Pow(2, i);
                command += GenerateSingleDeclaration(true, amountOfVariable);
                command += GenerateSingleDeclaration(false, amountOfVariable);
            }

            return command;
        }

        private static string GenerateSingleArray(bool isClass, double amountOfVariable)
        {
            var array = "";
            var name = isClass ? "C" : "S";
            array += "void P" + name + amountOfVariable + "()\n{";
            array += "var array= new " + name + amountOfVariable + "[Constants.ArraySize];";
            if (isClass)
                array += "for (int i = 0; i < Constants.ArraySize; i++) array[i] = new" + name + amountOfVariable +
                         "();";
            array += "}";
            return array;
        }

        private static string GenerateSingleCall()
        {
            return "";
        }

        public static string GenerateArrayRunner()
        {
            var command = "";
            command += "public class ArrayRunner : IRunner\n{\n";
            for (var i = 0; i < 10; i++)
            {
                var amountOfVariable = Math.Pow(2, i);
                command += GenerateSingleArray(true, amountOfVariable);
                command += GenerateSingleArray(false, amountOfVariable);
            }

            command += "public void Call(bool isClass, int size, int count)\n{\n";
            for (var i = 0; i < 10; i++)
            {
                var amountOfVariable = Math.Pow(2, i);
                command += "if (isClass && size == " + amountOfVariable + ")\n{";
                command += "for (int i = 0; i < count; i++) PC" + amountOfVariable + "();\nreturn;\n}\n";

                command += "if (!isClass && size == " + amountOfVariable + ")\n{";
                command += "for (int i = 0; i < count; i++) PS" + amountOfVariable + "();\nreturn;\n}\n";
            }

            command += "throw new ArgumentException();\n}\n}";
            return command;
        }

        public static string GenerateCallRunner()
        {
            var command = "";
            command += "public class CallRunner : IRunner\n{\n";
            for (var i = 0; i < 10; i++)
            {
                var amountOfVariable = Math.Pow(2, i);
                command += "void PC" + amountOfVariable + "(C" + amountOfVariable + " o) {}\n";
                command += "void PS" + amountOfVariable + "(S" + amountOfVariable + " o) {}\n";
            }

            command += "public void Call(bool isClass, int size, int count)\n{\n";
            for (var i = 0; i < 10; i++)
            {
                var amountOfVariable = Math.Pow(2, i);
                command += "if (isClass && size == " + amountOfVariable + ")\n{";
                command += "var o=new C" + amountOfVariable + "(); for (int i=0; i<count; i++) PC" + amountOfVariable +
                           "(o);\nreturn;\n}\n";

                command += "if (!isClass && size == " + amountOfVariable + ")\n{";
                command += "var o=new S" + amountOfVariable + "(); for (int i=0; i<count; i++) PS" + amountOfVariable +
                           "(o);\nreturn;\n}\n";
            }

            command += "throw new ArgumentException();}\n}";
            return command;
        }
    }
}