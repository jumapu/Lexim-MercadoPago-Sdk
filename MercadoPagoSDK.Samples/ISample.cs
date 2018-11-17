namespace MercadoPagoSDK.Samples
{
    internal interface ISample
    {
        string Category { get;  }
        string Name { get; }
        void Run();
    }
}