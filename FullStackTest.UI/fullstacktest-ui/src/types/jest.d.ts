// Type definitions for Jest matchers in Angular testing environment
import 'jest';

declare global {
  namespace jest {
    interface Expect {
      <T = any>(actual: T): JestMatchers<T>;
    }

    interface Matchers<R, T = {}> {
      toBe(expected: T): R;
      toEqual(expected: T): R;
      toStrictEqual(expected: T): R;
      toContain(expected: any): R;
      toMatch(expected: string | RegExp): R;
      toBeTruthy(): R;
      toBeFalsy(): R;
      toBeNull(): R;
      toBeUndefined(): R;
      toBeDefined(): R;
      toBeNaN(): R;
      toBeInstanceOf(expected: any): R;
      toHaveLength(expected: number): R;
      toThrow(expected?: string | RegExp | Error): R;
      toHaveBeenCalled(): R;
      toHaveBeenCalledTimes(expected: number): R;
      toHaveBeenCalledWith(...expected: any[]): R;
      toHaveBeenLastCalledWith(...expected: any[]): R;
      toHaveBeenNthCalledWith(n: number, ...expected: any[]): R;
    }

    interface JestMatchers<T> extends Matchers<void, T> {
      not: Matchers<void, T>;
    }
  }
}

export {};
