Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO

Namespace MyEncryptionDecryption
    Public Class clsEncryptDecrypt

        Public Function Encrypt(ByVal str As String) As String
            Dim encryptedString As String = ""
            Dim i As Integer = 0
            While i < str.Length
                encryptedString = encryptedString + Convert.ToChar(Convert.ToInt32(str(i)) + Convert.ToInt32(13))
                System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
            End While
            Return encryptedString
        End Function

        Public Function Decrypt(ByVal str As String) As String
            Dim decryptedString As String = ""
            Dim i As Integer = 0
            While i < str.Length

                decryptedString = decryptedString + Convert.ToChar(Convert.ToInt32(str(i)) - Convert.ToInt32(13))
                System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
            End While
            Return decryptedString
        End Function
    End Class
End Namespace

