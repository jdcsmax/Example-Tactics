namespace Assets.Zinnor.Supports
{
    public delegate TR Supplier<in T, out TR>(T t);
}