using Mauve.Compiler;
using Mauve.VirtualMachine;

const int stackSize = 1048576;
const int heapSize = 67108864;

var compiler = new Compiler();
compiler.GeneratePushI32(128);
compiler.GeneratePushI32(64);
compiler.GenerateSubI32();
compiler.GeneratePrintI32();
compiler.GeneratePushF64(12579.4f);
compiler.GeneratePrintF64();

var byteCode = compiler.GeneratedCode;

var virtualMachine = new VirtualMachine(stackSize, heapSize);
virtualMachine.Execute(byteCode);
