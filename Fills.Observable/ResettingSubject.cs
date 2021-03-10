using System;
using System.Reactive.Subjects;

namespace Fills
{
    public sealed class ResettingSubject<T> : ISubject<T>
    {
        private readonly Func<ISubject<T>> subjectFactory;

        private ISubject<T> currentSubject;


        public ResettingSubject(Func<ISubject<T>> subjectFactory)
        {
            this.subjectFactory = subjectFactory;
            this.currentSubject = subjectFactory();
        }


        public void OnCompleted()
        {
            var oldSubject = currentSubject;
            currentSubject = subjectFactory();
            oldSubject.OnCompleted();
        }

        public void OnError(Exception error)
        {
            var oldSubject = currentSubject;
            currentSubject = subjectFactory();
            oldSubject.OnError(error);
        }

        public void OnNext(T value)
        {
            currentSubject.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return currentSubject.Subscribe(observer);
        }
    }
}
