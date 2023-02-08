using Apps.Data;
using Apps.Models;
using System;
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
        private ApplicationDbContext _context;

        public ActDownloader(ApplicationDbContext context) : base()
        {
            _context = context;
        }

        public override void downloadAll(string year)
        {
            if("" == year)
            {
                year = DateTime.Now.Year.ToString();
            }

            string directoryPath = _path + year;

            /* Create directory for pdfs released in specified year */
            createDirectory(directoryPath);

            using (WebClient client = new WebClient())
            {
                int pdfId = 161;

                while(true)
                {
                    string fullPath = directoryPath + _directorySeparator + pdfId + _fileExtension;

                    if(!checkIfActAlreadyDownloaded(fullPath))
                    {
                        string downloadUrl = generateDownloadUrl(pdfId);
                        try
                        {
                            client.DownloadFile(downloadUrl, fullPath);

                            Pdf pdf = new Pdf();
                            pdf.Title = pdfId.ToString();
                            pdf.Path = fullPath;

                            Act act = new Act();
                            act.Title = pdfId.ToString();
                            act.Overview = "To be added.";
                            act.Points = 0;
                            act.Url = downloadUrl;
                            act.Pdf = pdf;

                            _context.Add(pdf);
                            _context.Add(act);
                            _context.SaveChanges();
                        }
                        catch(WebException ex)
                        {
                            break;
                        }
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

                if (!checkIfActAlreadyDownloaded(fullPath))
                {
                    try
                    {
                        client.DownloadFile(downloadUrl, fullPath);

                        Pdf pdf = new Pdf();
                        pdf.Title = pdfId.ToString();
                        pdf.Path = fullPath;

                        Act act = new Act();
                        act.Title = pdfId.ToString();
                        act.Overview = "To be added.";
                        act.Points = 0;
                        act.Url = downloadUrl;
                        act.Pdf = pdf;

                        _context.Add(pdf);
                        _context.Add(act);
                        _context.SaveChanges();
                    }
                    catch (WebException ex)
                    {
                    returnValue = 1;
                    }
                }
            }

            return returnValue;
        }

        public int downloadLink(string pdfLink)
        {
            int returnValue = 0;
            int year = DateTime.Now.Year;
            string directoryPath = _path + year;
            string pdfId = pdfLink.Substring(38, 3);
            int pdfIdi = int.Parse(pdfId);
            /* Create directory for pdfs released in specified year */
            createDirectory(directoryPath);

            using (WebClient client = new WebClient())
            {
                string downloadUrl = generateDownloadUrl(pdfIdi);
                string fullPath = directoryPath + _directorySeparator + pdfIdi + _fileExtension;

                if (!checkIfActAlreadyDownloaded(fullPath))
                {
                    try
                    {
                        client.DownloadFile(downloadUrl, fullPath);

                        Pdf pdf = new Pdf();
                        pdf.Title = pdfId.ToString();
                        pdf.Path = fullPath;

                        Act act = new Act();
                        act.Title = pdfId.ToString();
                        act.Overview = "To be added.";
                        act.Points = 0;
                        act.Url = downloadUrl;
                        act.Pdf = pdf;

                        _context.Add(pdf);
                        _context.Add(act);
                        _context.SaveChanges();
                    }
                    catch (WebException ex)
                    {
                        returnValue = 1;
                    }
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

        private bool checkIfActAlreadyDownloaded(string path)
        {
            return File.Exists(path);
        }

    }
}
