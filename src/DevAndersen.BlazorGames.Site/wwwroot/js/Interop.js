function setBeforeUnloadObject(obj)
{
    addEventListener('beforeunload', () =>
    {
        obj.invokeMethodAsync('OnBeforeUnload');
    });
}
