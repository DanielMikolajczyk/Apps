﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Apps.Services.Downloaders
{
    public class ActDownloader : Downloader
    {
        private string _downloadUrl = @"https://dziennikustaw.gov.pl/D";

        public ActDownloader() : base()
        {

        }

        public override void downloadAll()
        {
            DateTime date = DateTime.Now;
            int year = date.Year;

            string directoryPath = _path + year;

            /* Create directory for pdfs released in specified year */
            createDirectory(directoryPath);

            using (WebClient client = new WebClient())
            {
                int pdfId = 161;

                while(true)
                {
                    string downloadUrl = generateDownloadUrl(pdfId);
                    string fullPath = directoryPath + _directorySeparator + pdfId + _fileExtension;
                    try
                    {
                        client.DownloadFile(downloadUrl, fullPath);
                    }
                    catch(WebException ex)
                    {
                        break;
                    }
                    pdfId++;
                }
            }
        }

        /* Return 0 if ok, 1 otherwise */
        public override int download(int pdfId)
        {
            int returnValue = 0;
            int year = DateTime.Now.Year;
            string directoryPath = _path + year;

            /* Create directory for pdfs released in specified year */
            createDirectory(directoryPath);

            using (WebClient client = new WebClient())
            {
                string downloadUrl = generateDownloadUrl(pdfId);
                string fullPath = directoryPath + _directorySeparator + pdfId + _fileExtension;

                try
                {
                    client.DownloadFile(downloadUrl, fullPath);
                }
                catch (WebException ex)
                {
                    returnValue = 1;
                }
            }

            return returnValue;
        }

        /* 
         * Returns downloadUrl example: 
         * _downloadUrl : "https://dziennikustaw.gov.pl/D"
         * year         : "2023"
         * stringId     : "0000161"
         * constant     : "01"
         * Full path example : "https://dziennikustaw.gov.pl/D2023000016101.pdf"
         */
        private string generateDownloadUrl(int id)
        {
            string downloadUrl = _downloadUrl;
            string stringId = stringifyId(id);
            
            downloadUrl += stringId + _fileExtension;

            return downloadUrl;
        }

        /* Generate proper string version of passed document id */
        private string stringifyId(int id)
        {
            string returnString;
            string stringId = id.ToString();
            int year = DateTime.Now.Year;

            while (stringId.Length < 7)
            {
                stringId = "0" + stringId;
            }
            returnString = year + stringId + "01";

            return returnString;
        }

    }
}