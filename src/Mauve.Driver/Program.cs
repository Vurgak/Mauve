using Mauve.Compiler;
using Mauve.VirtualMachine;

const int stackSize = 1048576;
const int heapSize = 67108864;

var compiler = new Compiler();
compiler.GeneratePushI32(2137);
compiler.GeneratePrintI32();
compiler.GeneratePushI64(2137);
compiler.GeneratePrintI64();
compiler.GeneratePushF32(65432.54f);
compiler.GeneratePrintF32();
compiler.GeneratePushF64(65432.54);
compiler.GeneratePrintF64();

var byteCode = compiler.GeneratedCode;

var virtualMachine = new VirtualMachine(stackSize, heapSize);
virtualMachine.Execute(byteCode);
