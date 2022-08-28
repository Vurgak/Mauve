using Mauve.Compiler;
using Mauve.VirtualMachine;

const int stackSize = 1048576;
const int heapSize = 67108864;

var compiler = new Compiler();
compiler.GeneratePushI32(21);
var label = compiler.GeneratedCode.Length;
compiler.GeneratePushI32(42);
compiler.GeneratePrintI32();
compiler.GenerateJump(label);

var byteCode = compiler.GeneratedCode;

var virtualMachine = new VirtualMachine(stackSize, heapSize);
virtualMachine.Execute(byteCode);
