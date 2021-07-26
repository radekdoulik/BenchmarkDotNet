﻿using System;
using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Environments;
using Xunit;
using Xunit.Abstractions;

namespace BenchmarkDotNet.IntegrationTests
{
    public class ValuesReturnedByBenchmarkTest : BenchmarkTestExecutor
    {
        public ValuesReturnedByBenchmarkTest(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void AnyValueCanBeReturned() => CanExecute<ValuesReturnedByBenchmark>();

        public class ValuesReturnedByBenchmark
        {
#if NETFRAMEWORK
            [Benchmark]
            public System.Windows.Point? TypeFromCustomFrameworkAssembly() => new System.Windows.Point();

            [Benchmark]
            public Diagnostics.Windows.InliningDiagnoser TypeFromCustomDependency() => new Diagnostics.Windows.InliningDiagnoser();
#endif

            [Benchmark]
            public object ReturnNullForReferenceType() => null;

            [Benchmark]
            public object ReturnNotNullForReferenceType() => new object();

            [Benchmark]
            public DateTime? ReturnNullForNullableType() => null;

            [Benchmark]
            public DateTime? ReturnNotNullForNullableType() => DateTime.UtcNow;

            [Benchmark]
            public DateTime ReturnDefaultValueForValueType() => default;

            [Benchmark]
            public DateTime ReturnNonDefaultValueForValueType() => DateTime.UtcNow;

            [Benchmark]
            public Result<DateTime> ReturnGenericValueType() => new Result<DateTime>();

            [Benchmark]
            public Jit ReturnEnum() => Jit.RyuJit;

            private int intergerField = 123;
            [Benchmark]
            public ref int ReturnByRef() => ref intergerField;

            [Benchmark]
            public ref readonly int ReturnByReadonlyRef() => ref intergerField;

            public readonly struct ReadOnlyStruct { }
            private ReadOnlyStruct readOnlyStructField;

            [Benchmark]
            public ReadOnlyStruct ReturnReadOnlyStruct() => new ReadOnlyStruct();

            [Benchmark]
            public ref ReadOnlyStruct ReturnReadOnlyStructByRef() => ref readOnlyStructField;

            [Benchmark]
            public ref readonly ReadOnlyStruct ReturnReadOnlyStructByReadonlyRef() => ref readOnlyStructField;

            [Benchmark]
            public Span<byte> ReturnStackOnlyType() => new Span<byte>(Array.Empty<byte>());

            [Benchmark]
            public ImmutableArray<int> TypeFromNetStandardNuGetPackage() => ImmutableArray<int>.Empty;

            [Benchmark]
            public ValueTuple<int> TypeInTwoDlls() => new ValueTuple<int>();

            public struct Result<T>
            {
                public T Field;

                public Result(T field)
                {
                    Field = field;
                }
            }

            [Benchmark]
            public Job TypeCalledJob() => new Job();

            public class Job { }

            [Benchmark]
            public NoNamespace TypeWithoutNamespace() => new NoNamespace();

            [Benchmark]
            public unsafe void* PointerToAnything() => IntPtr.Zero.ToPointer();

            [Benchmark]
            public unsafe int* PointerToUnmanagedType() => (int*)IntPtr.Zero.ToPointer();
        }
    }
}

public class NoNamespace
{
}