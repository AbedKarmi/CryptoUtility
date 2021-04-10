
// RSA.CudafyEx
extern "C" __global__  void add( int* a, int aLen0,  int* b, int bLen0,  int* c, int cLen0);

// RSA.CudafyEx
extern "C" __global__  void add( int* a, int aLen0,  int* b, int bLen0,  int* c, int cLen0)
{
	int x = blockIdx.x;
	bool flag = x < 10;
	if (flag)
	{
		c[(x)] = a[(x)] + b[(x)];
	}
}
