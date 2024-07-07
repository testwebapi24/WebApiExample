using System.Security.Cryptography;
using System.Text;

namespace WebApiExample
{
    public class EncryptionHelper
    {
        public class RijndaelCrypt
        {

            /// <summary>
            /// 16-byte Private Key
            /// </summary>
            private static readonly byte[] IV = Encoding.UTF8.GetBytes("NEW1trpokFb3BVMm");


            #region Private/Protected Properties

            private byte[] PublicKey { get; set; }

            #endregion

            #region Private/Protected Methods

            #endregion

            #region Constructor

            /// <summary>C'tor</summary>
            /// <param name="publicKey"></param>
            public RijndaelCrypt(string publicKey)
            {
                //Encode digest
                var md5 = new MD5CryptoServiceProvider();
                PublicKey = md5.ComputeHash(Encoding.ASCII.GetBytes(publicKey));
            }

            #endregion

            #region Public Properties

            #endregion

            #region Public Methods

            /// <summary>
            /// Decryptor
            /// </summary>
            /// <param name="text">Base64 string to be decrypted</param>
            /// <returns></returns>
            public string Decrypt(string text)
            {
                //DateTime.Now.ToLocalTime()

                var cipher = new RijndaelManaged();
                try
                {

                    using (var decryptor = cipher.CreateDecryptor(PublicKey, IV))
                    {
                        var input = Convert.FromBase64String(text);

                        var newClearData = decryptor.TransformFinalBlock(input, 0, input.Length);

                        return Encoding.ASCII.GetString(newClearData);
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " +
                                      ae);
                    return null;
                }
                catch (ObjectDisposedException oe)
                {
                    Console.WriteLine("The object has already been disposed." + oe);
                    return null;
                }
                finally
                {
                    cipher.Dispose();
                }


            }

            /// <summary>
            /// Encryptor
            /// </summary>
            /// <param name="text">String to be encrypted</param>
            /// <returns></returns>
            public string Encrypt(string text)
            {
                var cipher = new RijndaelManaged();
                try
                {

                    using (var encryptor = cipher.CreateEncryptor(PublicKey, IV))
                    {
                        var buffer = Encoding.ASCII.GetBytes(text);
                        return Convert.ToBase64String(encryptor.TransformFinalBlock(buffer, 0, buffer.Length));
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " +
                                      ae);
                    return null;
                }
                catch (ObjectDisposedException oe)
                {
                    Console.WriteLine("The object has already been disposed." + oe);
                    return null;
                }
                finally
                {
                    cipher.Dispose();
                }

            }

            #endregion
        }
    }
}
